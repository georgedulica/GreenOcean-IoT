export interface IoTSystem {
  id: string;
  name: string;
  maxTemperature: number;
  minTemperature: number;
  maxHumidity: number;
  minHumidity: number;
  maxLightLevel: number;
  minLightLevel: number;
  timestamp: string;
  status: string;
  greenhouseId: string;
  systemId: string;
}
