import './styles';
import './core/array/quickSort';

const app = angular.module('sogeti-academy', [
    'ngMaterial',
    'ui.router',
    'ngFileUpload'
]);
import './application/ApplicationDirective';

app.config([
        '$mdThemingProvider',
        ($mdThemingProvider: angular.material.IThemingProvider) => {
            $mdThemingProvider.theme('default')
                .primaryPalette('red', {
                    default: '900'
                });
        }
    ]);
