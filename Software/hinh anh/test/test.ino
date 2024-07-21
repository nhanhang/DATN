void splitJsonString(const String& jsonString, String& part1, String& part2) {
  // Tìm vị trí của trường "vitriX"
  int pos = jsonString.indexOf(",\"vitriX\"");
  if (pos == -1) {
    // Nếu không tìm thấy "vitriX", sao chép toàn bộ chuỗi vào part1
    part1 = jsonString;
    part2 = "";  // part2 rỗng
    return;
  }

  // Tính độ dài phần 1
  part1 = jsonString.substring(0, pos);

  // Sao chép phần 2
  part2 = jsonString.substring(pos);
}

void setup() {
  // Khởi động cổng serial
  Serial.begin(115200);

  // Chuỗi JSON ban đầu
  String jsonString = "{\"cannang\":30,\"chieucao\":50,\"datetimetp\":\"02-06-2021\",\"diachiphong\":\"301\",\"diachitoa\":\"R4\",\"giong\":\"Cat\",\"mausac\":\"Black\",\"sodienthoai\":866513365,\"ten\":\"Lili\",\"tenchu\":\"Anthena\",\"tinhbiet\":\"Female\",\"tttiem\":0,\"vitriX\":0.0,\"vitriY\":0}";

  // Khởi tạo các biến để lưu trữ phần 1 và phần 2 của chuỗi JSON
  String part1;
  String part2;

  // Gọi hàm để tách chuỗi JSON
  splitJsonString(jsonString, part1, part2);

  // In kết quả ra Serial
  Serial.println("Phần 1: Trước trường vitriX");
  Serial.println(part1);
  Serial.println("\nPhần 2: Từ trường vitriX tới hết");
  Serial.println(part2);
}

void loop() {
  // Không cần làm gì trong hàm loop()
}
