import { sleep } from 'k6';
import http from 'k6/http'

export const options = {
    duration: "20s",
    vus: 15
};

export default function(){
    http.get('http://185.51.76.155:8090/')
    sleep(3)
}