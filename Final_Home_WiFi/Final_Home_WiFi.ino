#include <WiFi.h>

const char* ssid = "yourUser";
const char* password = "password";

WiFiServer server(80);

#include <ESP32Servo.h>
Servo m;  int a; int pr;
int redPin= 16;
int greenPin = 17;
int  bluePin = 18;

bool front_ledStatus=false,sensorStatus=true;



void setup() {
  Serial.begin(115200);
  delay(1000);
  
  pinMode(23, OUTPUT);
  
  WiFi.begin(ssid, password);
  
  while (WiFi.status() != WL_CONNECTED) {
    delay(1000);
    Serial.println("Connecting to WiFi...");
  }
  
  Serial.println("");
  Serial.println("WiFi connected.");
  Serial.println("IP address: ");
  Serial.println(WiFi.localIP());
  server.begin();
  Serial.println("Server started");

  pinMode(26,OUTPUT); //corredor
  pinMode(27,OUTPUT); //room1 large
  pinMode(32,OUTPUT); //front led
  pinMode(36,INPUT);  //photoresistor

  pinMode(redPin,  OUTPUT);              
  pinMode(greenPin, OUTPUT);
  pinMode(bluePin, OUTPUT);
  
  pinMode(redPin,  LOW);              
  pinMode(greenPin, LOW);
  pinMode(bluePin, LOW);
  m.attach(13);
  
m.write(80); 
}

void loop() {
  WiFiClient client = server.available();
  
  if (client) {
    Serial.println("Client connected");
    
    while (client.connected()) {
      if (client.available()) {
        String request = client.readStringUntil('\n');
        Serial.println("Received request: " + request);
        
        if (request.indexOf("LED_ON_Corridor") != -1) {
          digitalWrite(26, HIGH);
          //client.println("LED turned on");
        } else if (request.indexOf("LED_OFF_Corridor") != -1) {
          digitalWrite(26, LOW);
          //client.println("LED turned off");
        }
        else if (request.indexOf("LED_ON_Hotel") != -1) {
          digitalWrite(27, HIGH);
          //client.println("LED turned off");
        }
        else if (request.indexOf("LED_OFF_Hotel") != -1) {
          digitalWrite(27, LOW);
          //client.println("LED turned off");
        }
        else if (request.indexOf("LED_ON_Front") != -1) {
          digitalWrite(32, HIGH); front_ledStatus=true;
          //client.println("LED turned off");
        }
        else if (request.indexOf("LED_OFF_Front") != -1) {
          digitalWrite(32, LOW); front_ledStatus=false;
          //client.println("LED turned off");
        }
        else if (request.indexOf("OPEN_door") != -1) {
          m.write(40);
          //client.println("LED turned off");
        }
        else if (request.indexOf("CLOSE_door") != -1) {
          m.write(80);
          //client.println("LED turned off");
        }
        else if (request.indexOf("light-sensor_ON") != -1) {
          while(sensorStatus)
          {
            pr=analogRead(36); Serial.println(pr); delay(100);
            if (pr<700){digitalWrite(32, HIGH);sensorStatus=false;}
            
          }
          
          //client.println("LED turned off");
        }
        else if (request.indexOf("light-sensor_OFF") != -1 && front_ledStatus==false ) {
          digitalWrite(32, LOW); sensorStatus=true;
          //client.println("LED turned off");
        }
        else if (request.indexOf("living_room_ON") != -1) {
          setColor(5, 200, 250); // Blue Color
          //client.println("LED turned off");
        }
        else if (request.indexOf("living_room_OFF") != -1) {
          setColor(255, 255, 255); // White Color
          //client.println("LED turned off");
        }
        else if (request.indexOf("living_room_DarkBlue") != -1) {
          setColor(255, 255, 0); // Yellow Color
          //client.println("LED turned off");
        }
        else if (request.indexOf("living_room_GreenBlue") != -1) {
          setColor(255, 0, 0); // Red Color
          //client.println("LED turned off");
        }
        else if (request.indexOf("living_room_Blue") != -1) {
          setColor(0, 0, 255); // Blue Color
          //client.println("LED turned off");
        }
        else if (request.indexOf("living_room_RedGreen") != -1) {
          setColor(250, 0, 255); // Purple Color
          //client.println("LED turned off");
        }
        /*else if (request.indexOf(colorCode) != -1) {
             
          //client.println("LED turned off");
        }*/
        else {
          client.println("Invalid command");
        }
      }
    }
    client.stop();
    //close door when suddenly application exit
    m.write(80);
    
    Serial.println("Client disconnected");
  }

//  pr=analogRead(36);
//  if (pr<700){digitalWrite(32, HIGH);}
         /*digitalWrite(26,HIGH);
        digitalWrite(27,HIGH);
        digitalWrite(32,HIGH);
      
        setColor(255, 0, 0); // Red Color
        delay(1000);
        setColor(0,  255, 0); // Green Color
        delay(1000);
        setColor(0, 0, 255); // Blue Color
        delay(1000);
        setColor(255, 255, 255); // White Color
        delay(1000);
        setColor(170, 0, 255); // Purple Color
        delay(1000);
        setColor(127, 127,  127); // Light Blue
          delay(1000);
      
        pr=analogRead(36);
        Serial.println(pr);
        delay(100);
      */
      
//        m.write(80);
//        delay(1000);
//        m.write(40);
//        delay(500);
}

void setColor(int redValue, int greenValue,  int blueValue) {
  analogWrite(redPin, redValue);
  analogWrite(greenPin,  greenValue);
  analogWrite(bluePin, blueValue);
}
//
//  //for mix color request
//  void setColor_MIX(String hexColor) {
//  long number = (long) strtol( &hexColor[1], NULL, 16);
//  int red = number >> 16;
//  int green = number >> 8 & 0xFF;
//  int blue = number & 0xFF;
//  
//  analogWrite(redPin, red);
//  analogWrite(greenPin, green);
//  analogWrite(bluePin, blue);
//}
