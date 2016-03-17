export class ConfigService {
    get apiUrl(): string {
        return '$apiUrl$';
    }
}
angular.module('sogeti-academy')
    .service('ConfigService', ConfigService);