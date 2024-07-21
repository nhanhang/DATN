#include <ArduinoJson.h>
#include <HardwareSerial.h>
#include <sys/time.h>
#include "time.h"
#include <Wire.h>
#include <SPI.h>
#include <TinyGPSPlus.h>

//#define Serial2 SIM
#define SIM Serial2
String firebaseHost = "aerobic-canto-419813-default-rtdb.firebaseio.com";
HardwareSerial SerialGPS(1); 

double lat, lng;

double lat0, lng0;
int time_year, time_month, time_day, time_hour, time_min, time_sec;
//struct tm timeinfo;
unsigned long time_check=0;
TinyGPSPlus gps;
String latitude,longitude;
const int analogPin = 34; // GPIO 34
const float R1 = 15000.0; // 15k Ohm
const float R2 = 10000.0; // 10k Ohm
const float ADC_MAX = 4095.0; // Độ phân giải của ADC ESP32 (12-bit)
const float ADC_REF_VOLTAGE = 3.3; // Điện áp tham chiếu của ESP32
const float V_min = 6.5; // Điện áp tương ứng với 0%
const float V_max = 8.4; // Điện áp tương ứng với 100%

const int redPin = 5; // Chân điều khiển màu đỏ của LED RGB
const int greenPin = 18; // Chân điều khiển màu xanh lá của LED RGB
const int bluePin = 19; // Chân điều khiển màu xanh dương của LED RGB


const int pwmFrequency = 5000; // Tần số PWM
const int pwmResolution = 8; // Độ phân giải PWM (0-255)
const int redChannel = 0;
const int greenChannel = 1;
const int blueChannel = 2;

void PutData(String host, String path, String json);
bool checkGPS();
bool checkSIMModule();
bool CauhinhLTE();
bool CauhinhBangtan();
bool connectToNetwork();
void parseCLBSResponse(String response);
float calculateBatteryPercentage();
void setRGBColor(int red, int green, int blue);
void updateLEDColor(float batteryPercentage);

void setup() {
  // put your setup code here, to run once:
SerialGPS.begin(9600,SERIAL_8N1,27,26);
Serial.begin(115200);
  SIM.begin(115200);

Serial.println("Initializing SIM module...");
while (checkSIMModule() == false) {
    Serial.println("Failed to initialize SIM module.");
    Serial.println("Starting the sim module");
    delay(500);
  }

  Serial.println("Successfully started the SIM module");

  // while (CauhinhLTE() == false) {
  //   Serial.println("Configuration LTE failed");
  //   delay(500);
  // }

  // while (CauhinhBangtan() == false) {
  //   Serial.println("Band configuration failed");
  //   delay(500);
  // }

  // while (connectToNetwork() == false) {
  //   Serial.println("Connect network failed");
  //   delay(500);
  // }
  delay(1000);
  Serial2.print("ATE0\r\n");
  
  Serial.println("Configuration successful");
  delay(100);
  SerialGPS.println("$PMTK220,5000*1B");
delay(1000);
SerialGPS.println("$PMTK225,2,3000,1000,0,0*2C");
int a = 0;
lat0 = 0;
lng0=0;
lat=0; lng=0;
analogReadResolution(12); // Đặt độ phân giải của ADC là 12-bit
 ledcSetup(redChannel, pwmFrequency, pwmResolution);
  ledcSetup(greenChannel, pwmFrequency, pwmResolution);
  ledcSetup(blueChannel, pwmFrequency, pwmResolution);

  // Gán các chân PWM tương ứng với kênh
  ledcAttachPin(redPin, redChannel);
  ledcAttachPin(greenPin, greenChannel);
  ledcAttachPin(bluePin, blueChannel);
time_check = millis();
}

void loop() {
  // put your main code here, to run repeatedly:

  while (SerialGPS.available()) {

          //if(millis()-time_check > 5000){
// Serial2.print("AT+HTTPINIT\r\n");
//    delay(500);
//      PutData(firebaseHost,"3272832808/vitriX",  String(gps.location.lat(), 8));
     
//       PutData(firebaseHost,"3272832808/vitriY",String(gps.location.lng(), 8));
//       delay(500);
//       Serial2.println("AT+HTTPTERM");
    //   time_check = millis();
     // }
    if (gps.encode(SerialGPS.read())) {
 
      Serial.print("SATS: ");
      Serial.println(gps.satellites.value());
      Serial.print("LAT: ");
      lat = gps.location.lat();
      Serial.println(gps.location.lat(), 8);
      Serial.print("LONG: ");
      lng = gps.location.lng();
      Serial.println(gps.location.lng(), 8);
      Serial.print("ALT: ");
      Serial.println(gps.altitude.meters());
      Serial.print("SPEED: ");
      Serial.println(gps.speed.mps());
      if(lat!= lat0 || lng !=lng0){
        if(millis()-time_check>10000){
    Serial.println("ok");
     Serial2.print("AT+HTTPINIT\r\n");
    delay(1000);
      PutData(firebaseHost,"0831180364/vitriX",  String(lat, 8));
     
     delay(1000);
    
     Serial.println("ok2");
       PutData(firebaseHost,"0831180364/vitriY",String(lng,8));
       
       delay(1000);
       Serial2.println("AT+HTTPTERM");
         float batteryPercentage = calculateBatteryPercentage();
  
  Serial.print("Phần trăm pin còn lại: ");
  Serial.print(batteryPercentage);
  Serial.println(" %");
  
  updateLEDColor(batteryPercentage);
  
  delay(1000); // Đợi 1 giây trước khi đọc lại
       time_check = millis(); 
      }
       
lat0=lat; lng0= lng;
 }
    }
//  else {
//       //data = "";
//     SIM.println("AT+CLBS=4");  // Gửi lệnh AT
//       delay(1000);
//       String dataTime;
//       if (SIM.available()) {
//         dataTime = SIM.readString();
//       }
//       Serial.println( dataTime);
//       delay(1000);
//       parseCLBSResponse(dataTime);  
//       if(millis()-time_check >15000 ){
//       Serial2.print("AT+HTTPINIT\r\n");
//     delay(500);
//       PutData(firebaseHost,"0831180364/vitriX",  latitude);
     
//      delay(500);
    
//      Serial.println("ok2");
//        PutData(firebaseHost,"0831180364/vitriY",longitude);
       
//        delay(500);
//        Serial2.println("AT+HTTPTERM");
//          float batteryPercentage = calculateBatteryPercentage();
  
//   Serial.print("Phần trăm pin còn lại: ");
//   Serial.print(batteryPercentage);
//   Serial.println(" %");
  
//   updateLEDColor(batteryPercentage);
  
//   delay(1000); // Đợi 1 giây trước khi đọc lại
//        time_check = millis(); 
//  }
// }
//       // if (gps.date.isValid()) {
        
//       //   Serial.print("Date: ");
//       //   Serial.print(gps.date.day());
//       //   Serial.print("/");
//       //   Serial.print(gps.date.month());
//       //   Serial.print("/");
//       //   Serial.println(gps.date.year());
//       // } else {
//       //   Serial.println("Date: Invalid");
//       // }

//       // if (gps.time.isValid()) {
//       //   Serial.print("Hour: ");
//       //   Serial.print(gps.time.hour());
//       //   Serial.print(":");
//       //   Serial.print(gps.time.minute());
//       //   Serial.print(":");
//       //   Serial.println(gps.time.second());
//       // } else {
//       //   Serial.println("Hour: Invalid");
//       // }

//       // Serial.println("---------------------------");
      
    }
delay(1000);
  }

// Serial2.println("AT+HTTPINIT");
// delay(500);
// for(int i= 0 ; i< 11; i++){
//   delay(1000);
//   PutData(firebaseHost,"3272832808/vitriX", String(i));
//   Serial.println("ok");
//   }
//   delay(500);
//  Serial2.println("AT+HTTPTERM");
//delay(2000);
  
//}
void PutData(String host, String path, String json ){
  String receivedData;
  String url = "https://" + host + "/" + path + ".json";
  String command = "AT+HTTPPARA=\"URL\",\"" + url + "\"\r\n";

    Serial2.print(command);
  delay(1000);
Serial2.print("AT+HTTPPARA=\"CONTENT\",\"application/json\"\r\n");
  delay(1000);
  Serial2.print("AT+HTTPDATA=" + String(json.length()) + ",10000\r\n");
delay(500);
Serial2.println(json);
delay(500);
Serial2.println("AT+HTTPACTION=4");
delay(1000);

}

bool checkGPS(){
  SerialGPS.println("");
  delay(1000);
  if (SerialGPS.available()) {
    String response = SerialGPS.readString();
    Serial.println("Response: " + response);
    if (response.indexOf("") != -1) {
      return true;
    }
  }
  return false;
}

bool checkSIMModule() {
  Serial2.println("AT");  // Gửi lệnh AT
  delay(1000);        // Chờ phản hồi

  if (Serial2.available()) {
    String response = Serial2.readString();
    Serial.println("Response: " + response);
    if (response.indexOf("OK") != -1) {
      return true;
    }
  }
  return false;
}
bool CauhinhLTE() {
  Serial2.println("AT+CNMP= 38");  // Gửi lệnh AT
  delay(1000);                 // Chờ phản hồi

  if (Serial2.available()) {
    String response = Serial2.readString();
    Serial.println("Response: " + response);
    if (response.indexOf("OK") != -1) {
      return true;
    }
  }
  return false;
}
bool CauhinhBangtan() {
  Serial2.println("AT+CNBP=0x000700000FFF0380,0x000007FF3FDF3FFF");  // Gửi lệnh AT
  delay(1000);                                                   // Chờ phản hồi

  if (Serial2.available()) {
    String response = Serial2.readString();
    Serial.println("Response: " + response);
    if (response.indexOf("OK") != -1) {
      return true;
    }
  }
  return false;
}
bool connectToNetwork() {
  Serial2.println("AT+CREG?");  // Kiểm tra trạng thái đăng ký mạng
  delay(2000);
  if (Serial2.available()) {
    String response = Serial2.readString();
    Serial.println("Network registration response: " + response);
    if (response.indexOf("+CREG: 0,1") != -1 || response.indexOf("+CREG: 0,5") != -1) {
      return true;
    }
  }
  return false;
}
void parseCLBSResponse(String response) {
  // Kiểm tra xem phản hồi có chứa +CLBS không
  if (response.indexOf("+CLBS:") != -1) {
    int startIndex = response.indexOf(":") + 1;
    int endIndex = response.indexOf("\r", startIndex);
    String data = response.substring(startIndex, endIndex);
    
    // Tách các thành phần trong phản hồi
    int index1 = data.indexOf(",");
    int index2 = data.indexOf(",", index1 + 1);
    int index3 = data.indexOf(",", index2 + 1);
    int index4 = data.indexOf(",", index3 + 1);
    int index5 = data.indexOf(",", index4 + 1);
    
    String status = data.substring(0, index1);
     latitude = data.substring(index1 + 1, index2);
     longitude = data.substring(index2 + 1, index3);
    String accuracy = data.substring(index3 + 1, index4);
    String date = data.substring(index4 + 1, index5);
    String time = data.substring(index5 + 1);

    //Hiển thị thông tin định vị
    Serial.print("Status: ");
    Serial.println(status);
    Serial.print("Latitude: ");
    Serial.println(latitude);
    Serial.print("Longitude: ");
    Serial.println(longitude);
    Serial.print("Accuracy: ");
    Serial.println(accuracy);
    Serial.print("Date: ");
    Serial.println(date);
    Serial.print("Time: ");
    Serial.println(time);
  }
}
float calculateBatteryPercentage() {
  int a[100], temp;
  int adcValue;
  // Đọc giá trị ADC và lưu vào mảng
  for(int i = 0; i < 100; i++){
    adcValue = analogRead(analogPin); // Đọc giá trị ADC từ chân GPIO 34
    a[i] = adcValue;
    delay(100);
  }
  
  // Sắp xếp mảng bằng thuật toán Bubble Sort
  for(int i = 0; i < 100 - 1; i++) {
    for(int j = 0; j < 100 - i - 1; j++) {
      if (a[j] > a[j + 1]) {
        temp = a[j];
        a[j] = a[j + 1];
        a[j + 1] = temp;
      }
    }
  }
  
  // Lấy giá trị trung bình của 5 giá trị giữa
  adcValue = 0;
  for (int l = 48; l < 53; l++){
    adcValue += a[l];
  }
  adcValue /= 5;
  
  // Tính toán điện áp tại GPIO 34
  float voltageGPIO34 = (adcValue / ADC_MAX) * ADC_REF_VOLTAGE; 
  
  // Tính toán điện áp tổng của 2 cell pin
  float V_total = voltageGPIO34 * (R1 + R2) / R2;
  
  // Tính phần trăm pin
  float percentage = (V_total - V_min) / (V_max - V_min) * 100;
  if (percentage < 0) percentage = 0;
  if (percentage > 100) percentage = 100;

  return percentage;
 }
void setRGBColor(int red, int green, int blue) {
 ledcWrite(redChannel, red);
  ledcWrite(greenChannel, green);
  ledcWrite(blueChannel, blue);
}  

void updateLEDColor(float batteryPercentage) {
  // int redValue, greenValue, blueValue;

  
  // if (batteryPercentage <= 10) {
  //   redValue = 255;
  //   greenValue = 0;
  //   blueValue = 0;
  // } else if (batteryPercentage <= 45) {
  //   redValue = 255;
  //   greenValue = map(batteryPercentage, 10, 45, 0, 165); // Cam (đỏ: 255, xanh lá: 165)
  //   blueValue = 0;
  // } else if (batteryPercentage <= 70) {
  //   redValue = 255;
  //   greenValue = map(batteryPercentage, 45, 70, 165, 255); // Vàng (đỏ: 255, xanh lá: 255)
  //   blueValue = 0;
  // } else {
  //   redValue = map(batteryPercentage, 70, 100, 255, 0);
  //   greenValue = 255;
  //   blueValue = 0;
  // }
    int redValue = map(batteryPercentage, 0, 100, 255, 0);
  int greenValue = map(batteryPercentage, 0, 100, 0, 255);
  int blueValue = 0; // Không sử dụng màu xanh dương trong trường hợp này+
  setRGBColor(redValue, greenValue, blueValue);
}