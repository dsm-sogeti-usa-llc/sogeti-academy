import {INavigationState} from '../../core/INavigationState';
import {WelcomeState} from '../../welcome/states/WelcomeState';

export class SideNavService {
    getAllStates(): INavigationState[] {
        return [
            WelcomeState
        ];
    }
}

angular.module('sogeti-academy')
    .service('SideNavService', SideNavService);