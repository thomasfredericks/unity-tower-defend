#include <Arduino.h>
#include <MicroMs.h>

#include <MicroOscSlip.h>
// Le nombre 128 entre les < > ci-dessous est le nombre maximal d'octets réservés pour les messages entrants.
// Les messages sortants sont écrits directement sur la sortie et ne nécessitent pas de réservation d'octets supplémentaires.
MicroOscSlip<128> monOsc(&Serial);

/* #define MY_NAME "ESP32"
#define MY_OSC_UDP_PORT 7000

#define COMPUTER_NAME "MSI"
#define COMPUTER_OSC_UDP_PORT 8000

#include <MicroNet.h>
 */
#include <M5_PbHub.h>
M5_PbHub myPbHub;

// #include <M5_Encoder.h>
// M5_Encoder myEncoder;


void setup()
{
  Serial.begin(115200);

  Wire.begin();
/*
  MICRO_MS_LOOP_FOR(5000)
  {
    MICRO_MS_EVERY(100)
    {
      Serial.println("In the loop for 5 seconds, printing every 100 ms");
    }
  }
    */

    myPbHub.begin();
   // myEncoder.begin();
   myPbHub.setPixelCount(0, 1); // Channel 0, 1 pixel

}

void loop()
{
  // put your main code here, to run repeatedly:
  monOsc.onOscMessageReceived( [](MicroOscMessage& msg) {
    if ( msg.checkOscAddress( "/pixel" ) ) {
      int r = msg.nextAsInt();
      int g = msg.nextAsInt();
      int b = msg.nextAsInt();
      myPbHub.setPixelColor(0, 0, r, g, b);
      //digitalWrite(LED_BUILTIN, value);
    }
  });

  MICRO_MS_EVERY(20)
  {
    //Serial.println("Hello, world!");
    //Serial.println(millis());
    int maLectureAnalogique = myPbHub.analogRead(1); //analogRead(32);
    monOsc.sendInt( "/angle" , maLectureAnalogique);

    int maLectureBouton = myPbHub.digitalRead(0); //digitalRead(33);
    monOsc.sendInt( "/button" , maLectureBouton);
   
  }
}
