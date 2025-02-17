#include <ESP8266WiFi.h>
#include <ESP8266HTTPClient.h>
#include <ArduinoJson.h>

// Configuration Wi-Fi
const char* ssid = "Neji's Galaxy A22";  // SSID du Wi-Fi
const char* password = "clinisyS";       // Mot de passe du Wi-Fi

// URL de l'API ASP.NET Core
const char* serverUrl = "http://192.168.90.12:5137/api/weather"; 

// Variables pour stocker les données
float temperature = 0.0;
float pressure = 0.0;

void setup() {
  Serial.begin(115200);  // Communication avec le STM32F4 (RX sur GPIO3)
  
  // Connexion Wi-Fi avec timeout
  WiFi.begin(ssid, password);
  Serial.print("🔌 Connexion au Wi-Fi...");
  
  int attempts = 0;
  while (WiFi.status() != WL_CONNECTED && attempts < 20) {
    delay(500);
    Serial.print(".");
    attempts++;
  }

  if (WiFi.status() == WL_CONNECTED) {
    Serial.println("\n✅ Connecté au Wi-Fi !");
    Serial.print("📡 Adresse IP : ");
    Serial.println(WiFi.localIP());
  } else {
    Serial.println("\n❌ Impossible de se connecter au Wi-Fi. Redémarrage...");
    ESP.restart();
  }
}

void loop() {
  // Lire les données envoyées par le STM32F4
  if (Serial.available()) {
    String data = Serial.readStringUntil('\n'); // Lire jusqu'au saut de ligne
    parseData(data);
  }

  // Vérifier que le Wi-Fi est toujours actif
  if (WiFi.status() != WL_CONNECTED) {
    Serial.println("⚠️ Wi-Fi déconnecté, tentative de reconnexion...");
    WiFi.begin(ssid, password);
    delay(5000);
    return;
  }

  // Envoyer les données au serveur
  sendData();

  // Attendre 5 secondes avant le prochain envoi
  delay(5000);
}

// Fonction pour parser les données JSON reçues du STM32F4
void parseData(String data) {
  Serial.println("📥 Données reçues du STM32F4 : " + data);

  StaticJsonDocument<200> doc;
  DeserializationError error = deserializeJson(doc, data);

  if (error) {
    Serial.print("❌ Erreur JSON : ");
    Serial.println(error.c_str());
    return;
  }

  // Vérification des clés JSON avant d'assigner
  if (doc.containsKey("temp") && doc.containsKey("pressure")) {
    temperature = doc["temp"];
    pressure = doc["pressure"];
    Serial.print("🌡️ Température : ");
    Serial.println(temperature);
    Serial.print("🔵 Pression : ");
    Serial.println(pressure);
  } else {
    Serial.println("⚠️ Données JSON incomplètes !");
  }
}

// Fonction pour envoyer les données au serveur
void sendData() {
  WiFiClient client;
  HTTPClient http;

  // Construire les données JSON
  String jsonData;
  StaticJsonDocument<200> doc;
  doc["temperature"] = temperature;
  doc["pressure"] = pressure;
  serializeJson(doc, jsonData);

  Serial.println("📡 Envoi des données...");
  Serial.println("📤 JSON : " + jsonData);

  http.begin(client, serverUrl);
  http.addHeader("Content-Type", "application/json");

  int httpResponseCode = http.POST(jsonData);
  
  if (httpResponseCode > 0) {
    Serial.print("✅ Réponse HTTP : ");
    Serial.println(httpResponseCode);
    Serial.println("📝 Réponse du serveur :");
    Serial.println(http.getString());
  } else {
    Serial.print("❌ Erreur HTTP : ");
    Serial.println(httpResponseCode);
    Serial.println("🛑 Détail de l'erreur : " + http.errorToString(httpResponseCode));
  }

  http.end();
}
