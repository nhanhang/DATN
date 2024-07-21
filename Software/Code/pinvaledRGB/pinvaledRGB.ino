const int analogPin = 34; // GPIO 34
const float R1 = 15000.0; // 15k Ohm
const float R2 = 10000.0; // 10k Ohm
const float ADC_MAX = 4095.0; // Độ phân giải của ADC ESP32 (12-bit)
const float ADC_REF_VOLTAGE = 3.3; // Điện áp tham chiếu của ESP32
const float V_min = 7.4; // Điện áp tương ứng với 0%
const float V_max = 8.4; // Điện áp tương ứng với 100%

const int redPin = 5; // Chân điều khiển màu đỏ của LED RGB
const int greenPin = 18; // Chân điều khiển màu xanh lá của LED RGB
const int bluePin = 19; // Chân điều khiển màu xanh dương của LED RGB
const int pwmFrequency = 5000; // Tần số PWM
const int pwmResolution = 8; // Độ phân giải PWM (0-255)
const int redChannel = 0;
const int greenChannel = 1;
const int blueChannel = 2;
void setup() {
  Serial.begin(115200);
  analogReadResolution(12); // Đặt độ phân giải của ADC là 12-bit

  // pinMode(redPin, OUTPUT);
  // pinMode(greenPin, OUTPUT);
  // pinMode(bluePin, OUTPUT);
    ledcSetup(redChannel, pwmFrequency, pwmResolution);
  ledcSetup(greenChannel, pwmFrequency, pwmResolution);
  ledcSetup(blueChannel, pwmFrequency, pwmResolution);

  // Gán các chân PWM tương ứng với kênh
  ledcAttachPin(redPin, redChannel);
  ledcAttachPin(greenPin, greenChannel);
  ledcAttachPin(bluePin, blueChannel);
}

float calculateBatteryPercentage() {
  int a[100], temp;
  int adcValue;
  // Đọc giá trị ADC và lưu vào mảng
  for(int i = 0; i < 100; i++){
    adcValue = analogRead(34); // Đọc giá trị ADC từ chân GPIO 34
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
     Serial.print(percentage);
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
  int redValue = map(batteryPercentage, 0, 100, 255, 0);
  int greenValue = map(batteryPercentage, 0, 100, 0, 255);
  int blueValue = 0; // Không sử dụng màu xanh dương trong trường hợp này
  
  setRGBColor(redValue, greenValue, blueValue);
}

void loop() {
  float batteryPercentage = calculateBatteryPercentage();
  
  Serial.print("Phần trăm pin còn lại: ");
  Serial.print(batteryPercentage);
  Serial.println(" %");
  
  updateLEDColor(batteryPercentage);
  
  delay(1000); // Đợi 1 giây trước khi đọc lại
}
