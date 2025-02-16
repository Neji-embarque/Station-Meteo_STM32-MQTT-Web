#include <ESP8266WiFi.h>
#include <ESP8266HTTPClient.h>

// Configuration Wi-Fi
const char* ssid = "Neji's Galaxy A22";         // Remplacez par votre SSID Wi-Fi
const char* password = "clinisyS";              // Remplacez par votre mot de passe Wi-Fi

// URL de webhook.site
const char* serverUrl = "https://webhook.site/ef20b4b8-b298-4979-905f-436269bd2fc7";  // Remplacez par votre URL

void setup() {
  // Initialisation de la communication série
  Serial.begin(115200);

  // Connexion au Wi-Fi
  WiFi.begin(ssid, password);
  while (WiFi.status() != WL_CONNECTED) {
    delay(500);
    Serial.print(".");
  }
  Serial.println("\nConnecté au Wi-Fi");
  Serial.print("Adresse IP : ");
  Serial.println(WiFi.localIP());
}

void loop() {
  // Simuler des données à envoyer
  float temperature = 25.5;
  float pressure = 1013.25;

  // Créer un objet JSON
  String jsonData = "{\"temp\": " + String(temperature) + ", \"pressure\": " + String(pressure) + "}";

  // Envoyer les données au serveur
  if (WiFi.status() == WL_CONNECTED) {
    WiFiClient client;
    HTTPClient http;

    // Utiliser la nouvelle méthode begin avec WiFiClient
    Serial.println("Tentative de connexion au serveur...");
    http.begin(client, serverUrl);
    http.addHeader("Content-Type", "application/json");

    Serial.println("Envoi des données...");
    Serial.println("Données envoyées : " + jsonData);

    int httpResponseCode = http.POST(jsonData);
    if (httpResponseCode > 0) {
      Serial.print("Réponse du serveur : ");
      Serial.println(httpResponseCode);

      // Lire la réponse du serveur
      String response = http.getString();
      Serial.println("Réponse du serveur :");
      Serial.println(response);
    } else {
      Serial.print("Erreur lors de l'envoi : ");
      Serial.println(httpResponseCode);
      Serial.print("Message d'erreur : ");
      Serial.println(http.errorToString(httpResponseCode).c_str());
    }
    http.end();
  } else {
    Serial.println("Non connecté au Wi-Fi");
  }

  // Attendre 5 secondes avant d'envoyer de nouvelles données
  delay(5000);
}