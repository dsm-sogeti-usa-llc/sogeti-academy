import {INavigationState} from '../../core/INavigationState';
import {WelcomeState} from '../../welcome/states/WelcomeState';
import {TopicsState} from '../../topics/states/TopicsState';

export class SidenavService {
    getAllStates(): INavigationState[] {
        return [
            WelcomeState,
            TopicsState
        ];
    }
}

angular.module('sogeti-academy')
    .service('SidenavService', SidenavService);