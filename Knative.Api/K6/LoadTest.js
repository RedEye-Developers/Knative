import http from 'k6/http';
import { check } from 'k6';

export const options = {
  vus: 20,         
  duration: '1m',  
};

export default function () {
  const response = http.get('http://localhost:8080/weatherforecast', {
    headers: {
      'Host': 'knative-api.default.127.0.0.1.sslip.io'
    }
  });

  check(response, {
    'status is 200': (r) => r.status === 200,
    'response time < 500ms': (r) => r.timings.duration < 500,
  });
}