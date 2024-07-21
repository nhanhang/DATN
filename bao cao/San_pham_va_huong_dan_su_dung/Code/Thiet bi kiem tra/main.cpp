#include <ArduinoJson.h>
#include <HardwareSerial.h>
#include <Wire.h>
#include <SPI.h>
#include <Adafruit_PN532.h>
#include <Adafruit_GFX.h>
#include <Adafruit_ST7735.h>
#include <sys/time.h>
#include "time.h"

#define SIM Serial2

#define PN532_SCK 14
#define PN532_MOSI 13
#define PN532_SS 15
#define PN532_MISO 12
#define PN532_IRQ 26
#define PN532_RESET 25

#define TFT_CS 5     //---------------
#define TFT_RST 4    //---------------
#define TFT_DC 19    //---------------
#define TFT_MOSI 23  //---------------
#define TFT_SCLK 18  //---------------




Adafruit_ST7735 tft = Adafruit_ST7735(TFT_CS, TFT_DC, TFT_MOSI, TFT_SCLK, TFT_RST);

Adafruit_PN532 nfc(PN532_SCK, PN532_MISO, PN532_MOSI, PN532_SS);

#define BLACK 0x0000
#define BLUE 0x001F
#define RED 0xF800
#define GREEN 0x07E0
#define CYAN 0x07FF
#define MAGENTA 0xF81F
#define YELLOW 0xFFE0
#define WHITE 0xFFFF
#define ORAGNE 0xFD20
//HardwareSerial SIM(1); // use UART1
//String path = "123456/tenchu";
String firebaseHost = "aerobic-canto-419813-default-rtdb.firebaseio.com";

char apn[] = "v-internet";  // type your APN here
char user[] = "";
char pass[] = "";

String data = "";
volatile bool irqTriggered = false;
int cannang, chieucao, time_year, time_month, time_day, time_hour, time_min;
int time_year1, time_month1, time_day1, time_hour1, time_min1;
String datetimetp, diachiphong, diachitoa, giong, loaitotnghiep, mausac, namsinh, ten, tenchu, xuatxu, tinhbiet, tttiem;
long sodienthoai;
int check = 0;
int json = 0;
struct tm tm;
int statusDisplay=0;
unsigned long time_check=0;
float batteryPercentage1;
float batteryPercentage;
const int analogPin = 34; // GPIO 34
const float R1 = 15000.0; // 15k Ohm
const float R2 = 10000.0; // 10k Ohm
const float ADC_MAX = 4095.0; // Độ phân giải của ADC ESP32 (12-bit)
const float ADC_REF_VOLTAGE = 3.3; // Điện áp tham chiếu của ESP32
const float V_min = 6.6; // Điện áp tương ứng với 0%
const float V_max = 8.4; // Điện áp tương ứng với 100%

bool checkSIMModule();
bool CauhinhLTE();
bool CauhinhBangtan();
bool connectToNetwork();
int layData(String host, String path);
String layChuoiCon(String chuoiGoc, char kyTuBatDau);
void tachData(String chuoi, int& cannang, int& chieucao, String& datetimetp, String& diachiphong, String& diachitoa, String& giong, String& tinhbiet, String& mausac, long& sodienthoai, String& ten, String& tenchu);
void laythongtin(String path);
void printText(char* text, uint16_t color, int x, int y, int textSize);
void dienthongtin(String ID, String ten, String giong, String tinhbiet, int cannang, int chieucao, String datetimetp, String mausac, String tenchu, long sodienthoai, String diachiphong, String diachitoa);
void setLineColors(uint16_t color, uint16_t a, uint16_t b);
void manhinhchinh();
void displaySignalColumn(int columnNumber);
void pin(float phantram);
bool parseSIMTime(String response, struct tm &timeinfo);
void printCurrentTime(int e, int d, int c, int a, int b);
void printLocalTime(const struct tm &timeinfo);
void printDigits(int digits);
void drawLightning(int startX, int startY);
void clearLightning(int startX, int startY);
 bool getTimeSIM();
void canhbao1 ();
void canhbao2 ();
void tachChuoi(const String& input, String result[]);
void tachChuoi2(const String& input, String& chuoi1, String& chuoi2);
void guiSMS(String number, String message);
void SMScanhbao();
void PutData(String host, String path, String json );
String extractData(String input);
int countChar(char *str, char target);
int splitJsonString(String& jsonString);
String layrealtime ();
int splitJsonStringTime(String& jsonString);
String getRealTimeFromJson(const String& jsonResponse);
float calculateBatteryPercentage();
bool CauhinhTime();
void parseCLBSResponse(String response);
void CurrentTime();
void setSystemTime(String date, String time);

void setup() {
  // put your setup code here, to run once:
  Serial.begin(115200);
  SIM.begin(115200);
  nfc.begin();

  printText("Please wait...",BLUE,35,70,1);
  //SIM.begin(115200,SERIAL_8N1,27,26);
  Serial.println("Initializing SIM module...");
  delay(10000);

  while (checkSIMModule() == false) {
    Serial.println("Failed to initialize SIM module.");
    Serial.println("Starting the sim module");
    delay(500);
  }

  Serial.println("Successfully started the SIM module");
 
    while (CauhinhTime() == false) {
    Serial.println("Get Time of Sim failed");
    delay(500);
  }

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
  SIM.print("ATE0\r\n");
  
  Serial.println("Configuration successful");
  delay(100);
 
  Serial.println("Get Time successful");

  uint32_t versiondata = nfc.getFirmwareVersion();
  while (!versiondata) {
    
    versiondata = nfc.getFirmwareVersion();
    Serial.println("Didn't find PN53x board");
    delay(500);  // halt
  }
 nfc.SAMConfig();
  //nfc.setPassiveActivationRetries(0xFE);

  pinMode(27, OUTPUT);
  pinMode(22, OUTPUT);
  digitalWrite(27, 1);
      pinMode(33, OUTPUT);
pinMode(2, OUTPUT);

    delay(100);
    Serial.println("Warning successful");
    
 
  Serial.println("oke");
 
SMScanhbao();
  
    
  delay(500);

  tft.fillScreen(BLACK);
  struct timeval tv;
    gettimeofday(&tv, nullptr);

    // Convert to time_t
    time_t now = tv.tv_sec;
    
    // Convert to struct tm for local time
    struct tm tm_now;
    localtime_r(&now, &tm_now);
    time_year = tm_now.tm_year + 1900;
    time_month = tm_now.tm_mon + 1;
    time_day = tm_now.tm_mday;
    time_hour = tm_now.tm_hour;
    time_min = tm_now.tm_min;
    time_year1 = tm_now.tm_year + 1900;
    time_month1 = tm_now.tm_mon + 1;
    time_day1 = tm_now.tm_mday;
    time_hour1 = tm_now.tm_hour;
    time_min1 = tm_now.tm_min;
  manhinhchinh();
  statusDisplay=0;
  time_check = millis();
  batteryPercentage1 = calculateBatteryPercentage();
  batteryPercentage= batteryPercentage1;
  pin (batteryPercentage1);
}

void loop() {
  // put your main code here, to run repeatedly:
      
  boolean success;
  uint8_t uid[] = { 0, 0, 0, 0, 0, 0, 0 };  // Buffer to store the returned UID
  uint8_t uidLength;                        // Length of the UID (4 or 7 bytes depending on ISO14443A card type)

  success = nfc.readPassiveTargetID(PN532_MIFARE_ISO14443A, &uid[0], &uidLength);

  String ID1 = "";
  //Serial.println(String(timeinfo.tm_min) +"-"+ String(time_min) );
 

  

  if (success) {
    //digitalWrite(22, 1);
    uint32_t uidValue = (uint32_t)uid[0] << 24 | (uint32_t)uid[1] << 16 | (uint32_t)uid[2] << 8 | (uint32_t)uid[3];
    char uidString[11];  // 10 digits + null terminator
    sprintf(uidString, "%010lu", uidValue);
    ID1 = String(uidString);
    char charID1[ID1.length() + 1];
    ID1.toCharArray(charID1, ID1.length() + 1);
    tft.fillScreen(BLACK);
     printText("ID:",RED,62,30,3);
  printText(charID1,YELLOW,20,70,2);
  printText("Please wait...",BLUE,35,100,1);
    // Wait 1 second before continuing
    //Serial.println("ok");
    // delay(500);
    laythongtin(ID1);
    delay(500);
    if (check == 1) {
      if(json ==1){
SIM.print("AT+HTTPINIT\r\n");
  delay(500);
 
    layData(firebaseHost,ID1 + "/tttiem");
    delay(500);
SIM.println("AT+HTTPTERM");
 tttiem= extractData(data);
    }
      tft.fillScreen(BLACK);
      dienthongtin(ID1, ten, giong, tinhbiet, cannang, chieucao, datetimetp, mausac, tenchu, sodienthoai, diachiphong, diachitoa);
      SIM.print("AT+HTTPINIT\r\n");
  delay(2000);
 
       PutData(firebaseHost,"display",ID1);
        delay(1000);
SIM.println("AT+HTTPTERM");
       delay(10000);
      if(tttiem == "1"){
      digitalWrite(33,1);
     
      tft.fillScreen(BLACK);  
      canhbao1();
      }
      
      delay(5000);
      digitalWrite(33,0);
      statusDisplay = 1;
      data = "";
      check = 0;
      json =0;
    } else {
      tft.fillScreen(BLACK);
      digitalWrite(33, 1);
      canhbao2();
      delay(7000);
      digitalWrite(33, 0);
      statusDisplay = 1;
    }
    //digitalWrite(22, 0);
    CurrentTime();
  }
  else {
    
     Serial.println("Waitting for card");
    
  delay(500);
  }

  if(time_day1 != time_day){
    //SMScanhbao();
for (int dai = 86; dai >= 70; dai--) {
      for (int i = 20; i <= 140; i++) {
        tft.drawPixel(i, dai, BLACK);
      }
  }
  time_day = time_day1;
  }
 if( time_min1 != time_min ){

    time_year = time_year1 ;
    time_month = time_month1;
    time_day = time_day1;
    time_hour = time_hour1;
    time_min = time_min1;
        if(statusDisplay==0){
      for (int dai = 59; dai >= 35; dai--) {
      for (int i = 35; i <= 125; i++) {

        tft.drawPixel(i, dai, BLACK);
      }
      
    }
    manhinhchinh();
    batteryPercentage = calculateBatteryPercentage();
    pin(batteryPercentage);
    //Serial.println(batteryPercentage);
    if((batteryPercentage - batteryPercentage1)>2){
    drawLightning(125, 2);
  
    }
    if((batteryPercentage1 - batteryPercentage)>2)
      clearLightning(125,2);
    }
      batteryPercentage1 =batteryPercentage;

    
    

    //Serial.println("Wait card");
    //delay(500);
  }
  if(statusDisplay!=0){
    tft.fillScreen(BLACK);
    manhinhchinh();
    statusDisplay=0;
    batteryPercentage1 = calculateBatteryPercentage();
  batteryPercentage= batteryPercentage1;
  pin (batteryPercentage1);
    }
   
}

bool checkSIMModule() {
  SIM.println("AT");  // Gửi lệnh AT
  delay(1000);        // Chờ phản hồi

  if (SIM.available()) {
    String response = SIM.readString();
    Serial.println("Response: " + response);
    if (response.indexOf("OK") != -1) {
      return true;
    }
  }
  return false;
}
bool CauhinhLTE() {
  SIM.println("AT+CNMP= 38");  // Gửi lệnh AT
  delay(1000);                 // Chờ phản hồi

  if (SIM.available()) {
    String response = SIM.readString();
    Serial.println("Response: " + response);
    if (response.indexOf("OK") != -1) {
      return true;
    }
  }
  return false;
}
bool CauhinhBangtan() {
  SIM.println("AT+CNBP=0x000700000FFF0380,0x000007FF3FDF3FFF");  // Gửi lệnh AT
  delay(1000);                                                   // Chờ phản hồi

  if (SIM.available()) {
    String response = SIM.readString();
    Serial.println("Response: " + response);
    if (response.indexOf("OK") != -1) {
      return true;
    }
  }
  return false;
}
bool connectToNetwork() {
  SIM.println("AT+COPS?");
  delay(2000);
  if (SIM.available()) {
    String response = SIM.readString();
    Serial.println("Network registration response: " + response);
    // if (response.indexOf("+CREG: 0,1") != -1 || response.indexOf("+CREG: 0,5") != -1) {
    //   return true;
    // }
  }
  SIM.println("AT+CREG?");  // Kiểm tra trạng thái đăng ký mạng
  delay(2000);
  if (SIM.available()) {
    String response = SIM.readString();
    Serial.println("Network registration response: " + response);
    if (response.indexOf("+CREG: 0,1") != -1 || response.indexOf("+CREG: 0,5") != -1) {
      return true;
    }
  }
  return false;
}

int layData(String host, String path) {

  String receivedData;
  String url = "https://" + host + "/" + path + ".json";
  String command = "AT+HTTPPARA=\"URL\",\"" + url + "\"\r\n";
 SIM.print("ATE0\r\n");
  delay(1000);
  SIM.print(command);
  delay(1000);
  
  SIM.print("AT+HTTPACTION=0\r\n");
  receivedData = "";
  delay(1000);
  while (SIM.available()) {
    char c = SIM.read();
    receivedData += c;
  }
  Serial.println("ACTION=0: " + receivedData);
  String searchString = ",200,";
  String size;
  int lastIndex = receivedData.indexOf(searchString);
  if (lastIndex != -1) {
    size = receivedData.substring(lastIndex + searchString.length());
    Serial.println("line:"+ size);
  } else {
    Serial.println("Không tìm thấy chuỗi \",200,\" trong chuỗi đầu vào.");
    return 0;
  }

  receivedData = "";
  int kichthuoc;
  kichthuoc = size.toInt();
  SIM.print("AT+HTTPREAD=0," + size + "\r\n");
  delay(2500);
  while (SIM.available()) {
    data = SIM.readString();
    //receivedData += c;
  }
  
  while(data.indexOf("ERROR")!=-1){
    data = "";
    SIM.print("AT+HTTPREAD=0," + size + "\r\n");
    delay(2500);
    while (SIM.available()) {
    data = SIM.readString();
    //receivedData += c;
  }
   // return 0;
  }
  Serial.println("All data: " + data + ":end");

  //data = result;
  delay(500);

  return 1;
}

String layChuoiCon(String chuoiGoc, char kyTuBatDau) {

  String subString;
  int lastIndex = chuoiGoc.indexOf(kyTuBatDau);
  if (lastIndex != -1) {
    subString = chuoiGoc.substring(lastIndex);
    //Serial.println("Chuỗi từ kí tự '{' trở đi: " + subString);
    return subString;
  } else {
    Serial.println("Không tìm thấy kí tự '{' trong chuỗi đầu vào.");
    return "";
  }
}
// String layDongCuoiCung(String chuoi) {

//   int viTriXuongDongCuoiCung = chuoi.lastIndexOf("\n");  // Tìm vị trí của kí tự xuống dòng cuối cùng
//   if (viTriXuongDongCuoiCung != -1) {
//     // Nếu tìm thấy kí tự xuống dòng
//     return chuoi.substring(viTriXuongDongCuoiCung + 1);  // Lấy phần của chuỗi từ vị trí đó đến hết chuỗi
//   } else {
//     // Nếu không tìm thấy kí tự xuống dòng, trả về toàn bộ chuỗi
//     return chuoi;
//   }
// }
void tachData(String chuoi, int& cannang, int& chieucao, String& datetimetp, String& diachiphong, String& diachitoa, String& giong, String& tinhbiet, String& mausac, long& sodienthoai, String& ten, String& tenchu) {

  const size_t capacity = JSON_OBJECT_SIZE(13) + 500;
  //Serial.print("Chuỗi JSON đầu vào: ");
  //Serial.println(chuoi);
  DynamicJsonDocument doc(capacity);

  DeserializationError error = deserializeJson(doc, chuoi);
  if (error) {
    Serial.print(F("deserializeJson() failed: "));
    Serial.println(error.c_str());
    return;
  }

  cannang = doc["cannang"].as<int>();
  chieucao = doc["chieucao"].as<int>();
  datetimetp = doc["datetimetp"].as<String>();
  diachiphong = doc["diachiphong"].as<String>();
  diachitoa = doc["diachitoa"].as<String>();
  giong = doc["giong"].as<String>();
  mausac = doc["mausac"].as<String>();
  //namsinh = doc["namsinh"].as<String>();
  sodienthoai = doc["sodienthoai"].as<long>();
  ten = doc["ten"].as<String>();
  tenchu = doc["tenchu"].as<String>();
  tinhbiet = doc["tinhbiet"].as<String>();
  if(json == 0){
  tttiem = doc["tttiem"].as<int>();
  }
  
}

// String tachNOT(String data) {

//   String a;
//   int startQuoteIndex = data.indexOf('\"');
//   if (startQuoteIndex != -1) {
//     // Tìm vị trí của dấu nháy thứ hai "
//     int endQuoteIndex = data.indexOf('\"', startQuoteIndex + 1);
//     if (endQuoteIndex != -1) {
//       // Tách tên từ chuỗi dựa trên vị trí của dấu nháy
//       a = data.substring(startQuoteIndex + 1, endQuoteIndex);
//       return a;
//       // In ra tên đã tách
//       //Serial.println("Tên: " + a);
//     }
//   }
//   return "";
// }

void laythongtin(String path) {


  SIM.print("AT+HTTPINIT\r\n");
  delay(500);
  AB:
  while (layData(firebaseHost, path) == 0) {
    Serial.println("Retrieve data failed");
    delay(500);
  }
  Serial.println("Retrieve data successfully");
  //data = layDongCuoiCung(data);
  Serial.println("data: "+ data);
  data = layChuoiCon(data, '{');
  if (data != "") {

    if (data.indexOf("}") == -1) {
      json = splitJsonString(data);
      if(json == 2){
        goto AB;
      }
      data= data + "}";
    }
    Serial.println("data: " + data);
    delay(500);
    SIM.print("AT+HTTPTERM\r\n");
    check = 1;

    // delay(1000);
    // Serial.println("ok");

    tachData(data, cannang, chieucao, datetimetp, diachiphong, diachitoa, giong, tinhbiet, mausac, sodienthoai, ten, tenchu);
    // Serial.println("cannang: " + String(cannang));
    // Serial.println("chieucao: " + String(chieucao));
    // Serial.println("datetimetp: " + datetimetp);
    // Serial.println("diachiphong: " + diachiphong);
    // Serial.println("diachitoa: " + diachitoa);
    // Serial.println("giong: " + giong);
    // Serial.println("tinhbiet: " + tinhbiet);
    // Serial.println("mausac: " + mausac);
    // //Serial.println("namsinh: " + namsinh);
    // Serial.println("sodienthoai: " + String(sodienthoai));
    // Serial.println("ten: " + ten);
  } else {
    check = 0;
  }
  //delay(1000);
}

void printText(char* text, uint16_t color, int x, int y, int textSize) {
  tft.setCursor(x, y);
  tft.setTextColor(color);
  tft.setTextSize(textSize);
  tft.print(text);
}

void dienthongtin(String ID, String ten, String giong, String tinhbiet, int cannang, int chieucao, String datetimetp, String mausac, String tenchu, long sodienthoai, String diachiphong, String diachitoa) {

  String sdt = "0" + String(sodienthoai);
  String cn = String(cannang);
  String cc = String(chieucao);

  ten.replace("-", " ");
  giong.replace("-", " ");
  datetimetp.replace("-", "/");
  mausac.replace("-", " ");
  tenchu.replace("-", " ");

  char charID[ID.length() + 1];
  char charten[ten.length() + 1];
  char chargiong[giong.length() + 1];
  char chartinhbiet[tinhbiet.length() + 1];
  char charcannang[cn.length() + 1];
  char charchieucao[cc.length() + 1];
  char chardatetimetp[datetimetp.length() + 1];
  char charmausac[mausac.length() + 1];
  char chartenchu[tenchu.length() + 1];
  char charsdt[sdt.length() + 1];
  char chardiachiphong[diachiphong.length() + 1];
  char chardiachitoa[diachitoa.length() + 1];

  ID.toCharArray(charID, ID.length() + 1);
  ten.toCharArray(charten, ten.length() + 1);
  giong.toCharArray(chargiong, giong.length() + 1);
  tinhbiet.toCharArray(chartinhbiet, tinhbiet.length() + 1);
  cn.toCharArray(charcannang, cn.length() + 1);
  cc.toCharArray(charchieucao, cc.length() + 1);
  datetimetp.toCharArray(chardatetimetp, datetimetp.length() + 1);
  mausac.toCharArray(charmausac, mausac.length() + 1);
  tenchu.toCharArray(chartenchu, tenchu.length() + 1);
  sdt.toCharArray(charsdt, sdt.length() + 1);
  diachiphong.toCharArray(chardiachiphong, diachiphong.length() + 1);
  diachitoa.toCharArray(chardiachitoa, diachitoa.length() + 1);

  setLineColors(GREEN, 0, 13);
        printText("Pet information", RED, 35, 5, 1);
  printText("ID:", CYAN, 0, 20, 1);  //Hien thi thong tin kenh
  printText(charID, YELLOW, 21, 20, 1);
  printText("Name:", CYAN, 85, 20, 1);
  printText(charten, YELLOW, 112, 20, 1);
  printText("Species:", CYAN, 1, 31, 1);
  printText(chargiong, YELLOW, 51, 31, 1);
  printText("Sex:", CYAN, 85, 31, 1);
  printText(chartinhbiet, YELLOW, 106, 31, 1);
  printText("Weight:", CYAN, 1, 42, 1);
  printText(charcannang, YELLOW, 45, 42, 1);
  printText("Height:", CYAN, 85, 42, 1);
  printText(charchieucao, YELLOW, 130, 42, 1);
  printText("Color:", CYAN, 1, 64, 1);
  printText(charmausac, YELLOW, 39, 64, 1);
  printText("Vaccine", CYAN, 1, 53, 1);
  printText("date:", CYAN, 45, 53, 1);
  printText(chardatetimetp, YELLOW, 78, 53, 1);
  setLineColors(GREEN, 75, 88);
   printText("Pet owner information", RED, 17, 80, 1);
  printText("Owner:", CYAN, 1, 96, 1);
  printText(chartenchu, YELLOW, 39, 96, 1);
  printText("Phone", CYAN, 1, 107, 1);
  printText("Number:", CYAN, 33, 107, 1);
  printText(charsdt, YELLOW, 78, 107, 1);
  printText("Address:", CYAN, 1, 118, 1);
  printText(chardiachiphong, YELLOW, 51, 118, 1);
  printText("-", YELLOW, 67, 118, 1);
  printText(chardiachitoa, YELLOW, 73, 118, 1);
  printText("-Time City", YELLOW, 85, 118, 1);
}

void setLineColors(uint16_t color, uint16_t a, uint16_t b) {
  for (int y = a; y < b; y++) {
    tft.fillRect(0, y * 1, tft.width(), 5, color);
  }
}

void manhinhchinh(){
   
  displaySignalColumn(4);
 // pin(100);
  printText("4G",BLUE,40,6,1);
  printCurrentTime(time_year, time_month, time_day, time_hour, time_min);
  printText("Pet management equipment", YELLOW, 8, 97, 1);
  printText("Times City Apartment", CYAN, 20, 110, 1);
}

void displaySignalColumn(int columnNumber) {
  int a;
  uint16_t b;
  switch (columnNumber) {
    case 1:
      b = 0xF800;
      break;
    case 2:
      b = 0xFD20;
      break;
    case 3:
      b = 0x07E0;
      break;
    default:
      b = 0x07E0;
  }
  for (int i = 1; i < columnNumber + 1; i++) {
    a = 2 + 5;
    for (int rong = (i - 1) * a; rong < (i - 1) * a + 5; rong++) {
      for (int dai = 12; dai > 12 - i * 3; dai--) {
        tft.drawPixel(rong, dai, b);
      }
    }
    // Tính toán vị trí và kích thước của cột sóng
  }
}

void pin(float phantram) {
    for (int rong = 138; rong < 152 ; rong++) {
        //for (int dai = 10; dai >= 0 ; dai--) {
          tft.drawPixel(rong, 2, WHITE);
          tft.drawPixel(rong, 12, WHITE);
      // }
  }
  for (int dai = 12; dai >= 2 ; dai--) {
          tft.drawPixel(138, dai, WHITE);
          tft.drawPixel(152, dai, WHITE);
        }
  for (int rong = 152; rong < 155 ; rong++) {
      

        tft.drawPixel(rong, 8, WHITE);
        tft.drawPixel(rong, 5, WHITE);
  }
  for (int dai = 8; dai >= 5 ; dai--) {
          tft.drawPixel(152, dai, BLACK);
          tft.drawPixel(155, dai, WHITE);
        }
        //if(phantram>5){
  float a = phantram/7;
  // Serial.print("a: ");
  // Serial.println(a);
  uint16_t b;
  if(phantram<30){
    b = 0xF800;
  }
  else if(phantram<50){
    b = 0xFFE0;
  }
  else {
    b = 0x07E0;
  }
  for (int dai = 11; dai >= 3 ; dai--) {
    for(int i = 139; i<=138+a; i++){
      
          tft.drawPixel(i, dai, b);
    }
        }
  //}
}


bool parseSIMTime(String response, struct tm &timeinfo) {
  // Tìm vị trí của chuỗi +CCLK:
  int index = response.indexOf("+CCLK: \"");
  if (index == -1) {
    return false;
  }
  
  // Tách chuỗi thời gian
  String timeString = response.substring(index + 8, index + 8 + 17); // +8 để bỏ qua +CCLK: " và lấy 17 ký tự tiếp theo
  Serial.println("Time String: " + timeString);

  int year, month, day, hour, minute, second;
  if (sscanf(timeString.c_str(), "%d/%d/%d,%d:%d:%d", &year, &month, &day, &hour, &minute, &second) == 6) {
    tm.tm_year = year + 2000 - 1900; // Chuyển đổi từ năm 2000
    tm.tm_mon = month - 1; // Tháng từ 0
    tm.tm_mday = day;
    tm.tm_hour = hour;
    tm.tm_min = minute;
    tm.tm_sec = second;
    return true;
  }
  return false;
}

void printCurrentTime(int e, int d, int c, int a, int b) {
  char timeString[6]; // Dùng để lưu trữ chuỗi thời gian (hh:mm)
  char dateString[11];
  snprintf(timeString, sizeof(timeString), "%02d:%02d", a,b);
    snprintf(dateString, sizeof(dateString), "%02d-%02d-%04d", c,d,e);
  printText(timeString, RED, 35, 35, 3); // Hiển thị thời gian ở tọa độ (10, 10) với kích thước chữ 2
printText(dateString, BLUE, 20, 70, 2);

}

void printLocalTime(const struct tm &timeinfo) {
  Serial.print("Thời gian hiện tại: ");
  Serial.print(timeinfo.tm_year + 1900);
  Serial.print("-");
  printDigits(timeinfo.tm_mon + 1);
  Serial.print("-");
  printDigits(timeinfo.tm_mday);
  Serial.print(" ");
  printDigits(timeinfo.tm_hour);
  Serial.print(":");
  printDigits(timeinfo.tm_min);
  Serial.print(":");
  printDigits(timeinfo.tm_sec);
  Serial.println();
}

void printDigits(int digits) {
  if (digits < 10) {
    Serial.print("0");
  }
  Serial.print(digits);
}

void drawLightning(int startX, int startY) {
  // Vẽ các đường thẳng tạo thành hình dạng của sét (lật ngược)
  tft.drawLine(startX, startY + 7, startX + 2, startY + 2, ORAGNE);
  tft.drawLine(startX + 2, startY + 2, startX + 4, startY + 6, ORAGNE);
  tft.drawLine(startX + 4, startY + 6, startX + 3, startY + 1, ORAGNE);
  tft.drawLine(startX + 3, startY + 1, startX + 5, startY + 3, ORAGNE);
  tft.drawLine(startX + 5, startY + 3, startX + 6, startY, ORAGNE);
} 
void clearLightning(int startX, int startY) {
  // Vẽ các đường thẳng tạo thành hình dạng của sét (lật ngược)
  tft.drawLine(startX, startY + 7, startX + 2, startY + 2, BLACK);
  tft.drawLine(startX + 2, startY + 2, startX + 4, startY + 6, BLACK);
  tft.drawLine(startX + 4, startY + 6, startX + 3, startY + 1, BLACK);
  tft.drawLine(startX + 3, startY + 1, startX + 5, startY + 3, BLACK);
  tft.drawLine(startX + 5, startY + 3, startX + 6, startY, BLACK);
} 
  bool getTimeSIM(){
    //AB:
    String a =layrealtime();
    while(a == "0"){
      a =layrealtime();
    }
    a = layChuoiCon(a,'{');
 if (a != ""){
  if (data.indexOf("}") == -1) {
    // Serial.print("time: ");
    // Serial.println(a);
      json = splitJsonStringTime(a);

      if(json == 0){
        a= a + "}";
              
      }
      else {
        return false;
      }
      
  }
 }
// Serial.print("a: ");
// Serial.println(a);
 a = getRealTimeFromJson(a);
 
 if(a == "Error"){
  return false;
 }
   delay(500);
strptime(a.c_str(), "%Y-%m-%dT%H:%M:%S%z", &tm);
 time_t t = mktime(&tm);
   struct timeval tv;
  tv.tv_sec = t;
  tv.tv_usec = 0;
  settimeofday(&tv, NULL);
  return true;
}
  

void canhbao1 (){
  int16_t x_center = 80, y_center = 34; // Tâm của màn hình
  int16_t side_length = 30; // Chiều dài cạnh của tam giác đều (1/2 kích thước tam giác cũ)
  int16_t height = (sqrt(3) / 2) * side_length; // Chiều cao của tam giác đều
    int16_t x0 = x_center; // Điểm đỉnh của tam giác
  int16_t y0 = y_center - (height / 2); // Đỉnh của tam giác nằm trên tâm của màn hình
  int16_t x1 = x_center - (side_length / 2);
  int16_t y1 = y_center + (height / 2);
  int16_t x2 = x_center + (side_length / 2);
  int16_t y2 = y1;
    tft.fillTriangle(x0, y0, x1, y1, x2, y2, YELLOW);

  // Draw the exclamation mark
tft.fillRect(x_center - 2, 28, 5, 12, BLACK); // Phần đường thẳng của dấu chấm than (lớn hơn)
 tft.fillRect(x_center - 2, 41, 5, 5, BLACK); // Phần chấm của dấu chấm than (lớn hơn)
  printText("WARRING",RED,17,60,3);
 printText("Vnvaccinated pets",BLUE,29,100,1);
 //printText("Or unregistered",BLUE,35,104,1);
}
void canhbao2 (){
  int16_t x_center = 80, y_center = 34; // Tâm của màn hình
  int16_t side_length = 30; // Chiều dài cạnh của tam giác đều (1/2 kích thước tam giác cũ)
  int16_t height = (sqrt(3) / 2) * side_length; // Chiều cao của tam giác đều
    int16_t x0 = x_center; // Điểm đỉnh của tam giác
  int16_t y0 = y_center - (height / 2); // Đỉnh của tam giác nằm trên tâm của màn hình
  int16_t x1 = x_center - (side_length / 2);
  int16_t y1 = y_center + (height / 2);
  int16_t x2 = x_center + (side_length / 2);
  int16_t y2 = y1;
    tft.fillTriangle(x0, y0, x1, y1, x2, y2, YELLOW);

  // Draw the exclamation mark
tft.fillRect(x_center - 2, 28, 5, 12, BLACK); // Phần đường thẳng của dấu chấm than (lớn hơn)
 tft.fillRect(x_center - 2, 41, 5, 5, BLACK); // Phần chấm của dấu chấm than (lớn hơn)
  printText("WARRING",RED,17,60,3);
 //printText("unvaccinated or",BLUE,29,90,1);
 printText("unregistered pets",BLUE,29,100,1);
}
void tachChuoi(const String& input, String result[]) {
    int startIndex = 0;
    int lastIndex = 0;
    for (int i = 0; i < input.length(); i++) {
        if (input[i] == '-') {
            result[lastIndex++] = input.substring(startIndex, i);
            startIndex = i + 1;
        }
    }
    // Xử lý phần tử cuối cùng
    result[lastIndex] = input.substring(startIndex);
}

void tachChuoi2(const String& input, String& chuoi1, String& chuoi2) {
    int colonIndex = input.indexOf(':');
    if (colonIndex != -1) {
        chuoi1 = input.substring(0, colonIndex);
        chuoi2 = input.substring(colonIndex + 1);
    } else {
        // Trong trường hợp không tìm thấy dấu ':', gán hai chuỗi bằng chuỗi đầu vào
        chuoi1 = input;
        chuoi2 = "";
    }
}

void guiSMS(String number, String message){

  
  // Gửi tin nhắn
    SIM.print("AT+CMGS=\"");
  SIM.print(number);
  SIM.print("\"\r");// Thay thế số điện thoại bằng số bạn muốn gửi
  delay(1000);
  SIM.print(message);
  SIM.print("\x1A");
  delay(1000);
}
void SMScanhbao(){
  data = "";

    SIM.println("AT+CMGF=1");
  delay(1000);
  SIM.print("AT+HTTPINIT\r\n");
  delay(2000);
 while (layData(firebaseHost, "canhbao") == 0) {
    Serial.println("Retrieve data failed");
    delay(500);
  }
  data = layChuoiCon(data, '{');

  const size_t capacity = JSON_OBJECT_SIZE(13) + 500;
  DynamicJsonDocument doc(capacity);
  DeserializationError error = deserializeJson(doc, data);

  if (error) {
    Serial.print("deserializeJson() failed: ");
    Serial.println(error.c_str());
    //return;
  }
  // if (data != "") {

  //   if (data.indexOf("}") == -1) {
  //     data = data + '"' + ":" + "0" + '}';
  //   }
  // }
  // Extract cbtp1 and cbtp2
  String cbtp1 = doc["cbtp1"].as<String>();
  String cbtp2 = doc["cbtp2"].as<String>();
 data = "";
 while (layData(firebaseHost,"canhbao/cbtp3") == 0) {
    Serial.println("Retrieve data failed");
    delay(500);
  }
  
 String cbtp3= extractData(data);
 data = "";
  while (layData(firebaseHost,"canhbao/cbtp4") == 0) {
    Serial.println("Retrieve data failed");
    delay(500);
  }
 String cbtp4= extractData(data);
 if(cbtp1 != "0"){
 char Charcbtp1[cbtp1.length()+1];
 
 cbtp1.toCharArray(Charcbtp1,cbtp1.length()+1);
 int daugach1 = countChar(Charcbtp1,'-');
//  Serial.print("daugach: ");
//   Serial.println(daugach);
 String result1[daugach1 + 1];
 String ID1[daugach1 + 1];
 String SDT1[daugach1 + 1];
 tachChuoi(cbtp1, result1);

   for (int i = 0; i <= daugach1; i++) {
    tachChuoi2(result1[i],ID1[i],SDT1[i]);
  }
  for (int i = 0; i <= daugach1; i++) {
      guiSMS(SDT1[i],"Pets with ID: "+ID1[i]+" are due for vaccination in 7 days. Please have your pets vaccinated to ensure their health.");
      PutData(firebaseHost,ID1[i]+"/zcbtp1","0");
  }
 }
  if(cbtp2 != "0"){
 char Charcbtp2[cbtp2.length()+1];
 
 cbtp2.toCharArray(Charcbtp2,cbtp2.length()+1);
 int daugach2 = countChar(Charcbtp2,'-');
//  Serial.print("daugach: ");
//   Serial.println(daugach);
 String result2[daugach2 + 1];
 String ID2[daugach2 + 1];
 String SDT2[daugach2 + 1];
 tachChuoi(cbtp2, result2);

   for (int i = 0; i <= daugach2; i++) {
    tachChuoi2(result2[i],ID2[i],SDT2[i]);
  }
  for (int i = 0; i <= daugach2; i++) {
      guiSMS(SDT2[i],"Pets with ID: "+ID2[i]+" are due for vaccination in 3 days. Please have your pets vaccinated to ensure their health.");
      PutData(firebaseHost,ID2[i]+"/zcbtp2","0");
  }
 }
  if(cbtp3 != "0"){
 char Charcbtp3[cbtp3.length()+1];
 
 cbtp3.toCharArray(Charcbtp3,cbtp3.length()+1);
 int daugach3= countChar(Charcbtp3,'-');
//  Serial.print("daugach: ");
//   Serial.println(daugach);
 String result3[daugach3 + 1];
 String ID3[daugach3 + 1];
 String SDT3[daugach3 + 1];
 tachChuoi(cbtp3, result3);

   for (int i = 0; i <= daugach3; i++) {
    tachChuoi2(result3[i],ID3[i],SDT3[i]);
  }
  for (int i = 0; i <= daugach3; i++) {
      guiSMS(SDT3[i],"Pets with ID: "+ID3[i]+" are due for vaccinations after tomorrow. Please have your pets vaccinated to ensure their health.");
      PutData(firebaseHost,ID3[i]+"/zcbtp3","0");
  }
 }
   if(cbtp4 != "0"){
 char Charcbtp4[cbtp4.length()+1];
 
 cbtp4.toCharArray(Charcbtp4,cbtp4.length()+1);
 int daugach4= countChar(Charcbtp4,'-');
//  Serial.print("daugach: ");
//   Serial.println(daugach);
 String result4[daugach4 + 1];
 String ID4[daugach4 + 1];
 String SDT4[daugach4 + 1];
 tachChuoi(cbtp4, result4);

   for (int i = 0; i <= daugach4; i++) {
    tachChuoi2(result4[i],ID4[i],SDT4[i]);
  }
  for (int i = 0; i <= daugach4; i++) {
      guiSMS(SDT4[i],"Pets with ID: "+ID4[i]+" A has missed its vaccination schedule. Please have your pets vaccinated to ensure their health.");
      PutData(firebaseHost,ID4[i]+"/zcbtp4","0");
  }
 }
delay(500);
SIM.println("AT+HTTPTERM");
}

void PutData(String host, String path, String json ){
  String receivedData;
  String url = "https://" + host + "/" + path + ".json";
  String command = "AT+HTTPPARA=\"URL\",\"" + url + "\"\r\n";

    SIM.print(command);
  delay(1000);
Serial2.print("AT+HTTPPARA=\"CONTENT\",\"application/json\"\r\n");
  delay(1000);
  Serial2.print("AT+HTTPDATA=" + String(json.length()) + ",10000\r\n");
delay(500);
SIM.println(json);
delay(500);
SIM.println("AT+HTTPACTION=4");
delay(1000);


}

String extractData(String input) {
  int startIndex = input.indexOf("\"");
  int endIndex = input.lastIndexOf("\"");
  if (startIndex != -1 && endIndex != -1 && startIndex < endIndex) {
    return input.substring(startIndex + 1, endIndex);
  }
  return "";
}
int countChar(char *str, char target) {
    int count = 0;
    while (*str) {
        if (*str == target) {
            count++;
        }
        str++;
    }
    return count;
}
int splitJsonString(String& jsonString) {
  // Tìm vị trí của trường "vitriX"
  int pos = jsonString.indexOf(",\"vitriX\"");

  if (pos == -1) {
    // Nếu không tìm thấy "vitriX", sao chép toàn bộ chuỗi vào part1
    pos = jsonString.indexOf(",\"tttiem\"");
    if(pos == -1){
      return 2;
    }
    return 1;
    //part2 = "";  // part2 rỗng
    //return;
  }

  // Tính độ dài phần 1
  jsonString = jsonString.substring(0, pos);
  return 0;
  // Sao chép phần 2
  //part2 = jsonString.substring(pos);
}
String layrealtime (){
  String DataRealTime;
  SIM.print("AT+HTTPINIT\r\n");
  delay(500);
   SIM.println("AT+HTTPPARA=\"URL\",\"http://worldtimeapi.org/api/timezone/Etc/UTC\"");
   delay(1000);
SIM.print("AT+HTTPACTION=0\r\n");
delay(1500);
String receivedData;
while (SIM.available()) {
    char c = SIM.read();
    receivedData += c;
  }
  Serial.println("ACTION=0: " + receivedData);
  String searchString = ",200,";
  String size;
  int lastIndex = receivedData.indexOf(searchString);
  if (lastIndex != -1) {
    size = receivedData.substring(lastIndex + searchString.length());
    Serial.println("line:"+ size);
  } else {
    Serial.println("Không tìm thấy chuỗi \",200,\" trong chuỗi đầu vào.");
    return "0";
  }
  receivedData = "";
  int kichthuoc;
  kichthuoc = size.toInt();
  SIM.print("AT+HTTPREAD=0," + size + "\r\n");
  delay(2500);
  while (SIM.available()) {
    DataRealTime = SIM.readString();
    //receivedData += c;
  }
    while(DataRealTime.indexOf("ERROR")!=-1){
    DataRealTime = "";
    SIM.print("AT+HTTPREAD=0," + size + "\r\n");
    delay(2500);
    while (SIM.available()) {
    DataRealTime = SIM.readString();
    //receivedData += c;
  }
  }
  Serial.println("All data: " + DataRealTime + ":end");
  delay(500);
  SIM.println("AT+HTTPTERM");
  return DataRealTime;
}
int splitJsonStringTime(String& jsonString) {
  // Tìm vị trí của trường "vitriX"
  int pos = jsonString.indexOf(",\"day_of_week\"");

  if (pos == -1) {
    // Nếu không tìm thấy "vitriX", sao chép toàn bộ chuỗi vào part1
    // pos = jsonString.indexOf(",\"tttiem\"");
    // if(pos == -1){
    //   return 2;
    // }
    return 1;
    //part2 = "";  // part2 rỗng
    //return;
  }

  // Tính độ dài phần 1
  jsonString = jsonString.substring(0, pos);
  return 0;
  // Sao chép phần 2
  //part2 = jsonString.substring(pos);
}
String getRealTimeFromJson(const String& jsonResponse) {
   StaticJsonDocument<512> doc; // Tăng kích thước để đảm bảo đủ bộ nhớ
  DeserializationError error = deserializeJson(doc, jsonResponse);
  
  if (error) {
    Serial.print("deserializeJson() failed: ");
    Serial.println(error.f_str());
    return "Error";
  }
  
  const char* datetime = doc["datetime"];
  String utcTime = String(datetime);

  // Phân tích cú pháp chuỗi thời gian
  int year = utcTime.substring(0, 4).toInt();
  int month = utcTime.substring(5, 7).toInt();
  int day = utcTime.substring(8, 10).toInt();
  int hour = utcTime.substring(11, 13).toInt();
  int minute = utcTime.substring(14, 16).toInt();
  int second = utcTime.substring(17, 19).toInt();

  // Điều chỉnh múi giờ +7
  hour += 7;
  if (hour >= 24) {
    hour -= 24;
    day += 1;
    if (day > 30) { // Đơn giản hóa việc kiểm tra cuối tháng (không chính xác cho tất cả các tháng)
      day = 1;
      month += 1;
      if (month > 12) {
        month = 1;
        year += 1;
      }
    }
  }

  // Tạo chuỗi thời gian mới
  char localTime[30]; // Tăng kích thước để đảm bảo đủ bộ nhớ
  sprintf(localTime, "%04d-%02d-%02dT%02d:%02d:%02d+07:00", year, month, day, hour, minute, second);

  return String(localTime);
}
float calculateBatteryPercentage() {
  int a[30], temp;
  int adcValue;
  // Đọc giá trị ADC và lưu vào mảng
  for(int i = 0; i < 30; i++){
    adcValue = analogRead(analogPin); // Đọc giá trị ADC từ chân GPIO 34
    a[i] = adcValue;
    delay(100);
  }
  
  // Sắp xếp mảng bằng thuật toán Bubble Sort
  for(int i = 0; i < 30 - 1; i++) {
    for(int j = 0; j < 30 - i - 1; j++) {
      if (a[j] > a[j + 1]) {
        temp = a[j];
        a[j] = a[j + 1];
        a[j + 1] = temp;
      }
    }
  }
  
  // Lấy giá trị trung bình của 5 giá trị giữa
  adcValue = 0;
  for (int l = 13; l < 18; l++){
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
bool CauhinhTime(){
    SIM.println("AT+CLBS=4");  // Gửi lệnh AT
      delay(2000);
      String dataTime;
      if (SIM.available()) {
        dataTime = SIM.readString();
      }
      Serial.println(dataTime);
      delay(1000);
      if (dataTime.indexOf("+CLBS:") == -1) {
        return false;
      }
      parseCLBSResponse(dataTime);
      
      return true;
      
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
    String latitude = data.substring(index1 + 1, index2);
    String longitude = data.substring(index2 + 1, index3);
    String accuracy = data.substring(index3 + 1, index4);
    String date = data.substring(index4 + 1, index5);
    String time = data.substring(index5 + 1);

    // Hiển thị thông tin định vị
    // Serial.print("Status: ");
    // Serial.println(status);
    // Serial.print("Latitude: ");
    // Serial.println(latitude);
    // Serial.print("Longitude: ");
    // Serial.println(longitude);
    // Serial.print("Accuracy: ");
    // Serial.println(accuracy);
    // Serial.print("Date: ");
    // Serial.println(date);
    // Serial.print("Time: ");
    // Serial.println(time);

    // Gán thời gian hệ thống
    setSystemTime(date, time);
  }
}

void CurrentTime() {
    struct timeval tv;
    gettimeofday(&tv, nullptr);

    // Convert to time_t
    time_t now = tv.tv_sec;
    
    // Convert to struct tm for local time
    struct tm tm_now;
    localtime_r(&now, &tm_now);
    time_year1 = tm_now.tm_year + 1900;
     time_month1 = tm_now.tm_mon + 1;
      time_day1= tm_now.tm_mday;
       time_hour1= tm_now.tm_hour;
        time_min1=tm_now.tm_min;
       
    //     Serial.print("Current time: ");
    // Serial.print(time_year1); Serial.print("-");
    // Serial.print(time_month1); Serial.print("-");
    // Serial.print(time_day1); Serial.print(" ");
    // Serial.print(time_hour1); Serial.print(":");
    // Serial.println(time_min1); //Serial.print(":");
    //Serial.println(second);
    // Print the current time
    // std::cout << "Current time: "
    //           << std::put_time(&tm_now, "%Y-%m-%d %H:%M:%S") << std::endl;
}

void setSystemTime(String date, String time) {
  // Phân tích chuỗi ngày
  int year = date.substring(0, 4).toInt();
  int month = date.substring(5, 7).toInt();
  int day = date.substring(8, 10).toInt();

  // Phân tích chuỗi thời gian
  int hour = time.substring(0, 2).toInt();
  int minute = time.substring(3, 5).toInt();
  int second = time.substring(6, 8).toInt();

  // Thiết lập cấu trúc timeval
  struct tm tm;
  tm.tm_year = year - 1900; // Năm tính từ 1900
  tm.tm_mon = month - 1;    // Tháng tính từ 0
  tm.tm_mday = day;
  tm.tm_hour = hour;
  tm.tm_min = minute;
  tm.tm_sec = second;
  time_t t = mktime(&tm);

  // Điều chỉnh thời gian theo múi giờ GMT+7
  t += 7 * 3600; // 7 giờ tính bằng giây

  struct timeval now = { .tv_sec = t };
  settimeofday(&now, NULL);

  Serial.println("System time updated with GPS time.");
}

//   bool getTimeSIM(){
//     SIM.println("AT+CCLK?\r\n");  // Gửi lệnh AT
//     delay(1000);
//     String dataTime;
//     if (SIM.available()) {
//       dataTime = SIM.readString();
//     }

//   Serial.println("dataTime: "+dataTime);
//   if (parseSIMTime(dataTime, timeinfo)) {
//   time_t t = mktime(&timeinfo);
//   struct timeval now = { t, 0 };
//   settimeofday(&now, NULL);
//   printLocalTime(timeinfo); // Truyền vào biến timeinfo đã được phân tích
//   return true;
// } else {
//   Serial.println("Không thể phân tích thời gian từ phản hồi của mô-đun SIM");
//   return false;
// }