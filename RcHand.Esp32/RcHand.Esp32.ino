#include <Wire.h>
#include <Adafruit_PWMServoDriver.h>

Adafruit_PWMServoDriver pwm = Adafruit_PWMServoDriver(0x40); // I2C address

const int servoChannels[5] = { 0, 1, 2, 3, 4 }; // PCA9685 channels

const int servoMinUs = 500;   // Safe and common for most servos
const int servoMaxUs = 2500;

int usToPwmTicks(int us) {
    return map(us, 0, 20000, 0, 4095); // 20 ms = 50 Hz
}

void setup() {
    Serial.begin(115200);
    pwm.begin();
    pwm.setPWMFreq(50); // 50 Hz for servos
    delay(10);
}

void setServoAngle(uint8_t channel, int angle) {
    angle = constrain(angle, 0, 180);
    int pulseUs = map(angle, 0, 180, servoMinUs, servoMaxUs);
    int pwmVal = usToPwmTicks(pulseUs);
    pwm.setPWM(channel, 0, pwmVal);
}

void loop() {
    if (Serial.available()) {
        String message = Serial.readStringUntil('\n');
        message.trim();

        int start = 0;
        while (start < message.length()) {
            int sep = message.indexOf(';', start);
            String part = message.substring(start, sep == -1 ? message.length() : sep);
            int colon = part.indexOf(':');
            if (colon != -1) {
                int id = part.substring(0, colon).toInt();
                int val = part.substring(colon + 1).toInt();

                if (id >= 0 && id < 5) {
                    setServoAngle(servoChannels[id], val);
                }
                else if (id == 9) {
                    delay(val);
                }
            }
            start = sep + 1;
            if (sep == -1) break;
        }
    }
}
