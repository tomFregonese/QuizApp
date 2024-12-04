const protocol: string = 'http';
const host: string = 'localhost';
const port: string = '5001';
const apiVersion: string = 'v1';
const apiUrl: string = protocol + '://' + host + ':' + port + '/' + apiVersion + '/';

export const environment = {
    production: false,
    apiUrl: apiUrl
}

