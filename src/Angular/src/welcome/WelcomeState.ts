import {INavigationState} from '../core/INavigationState';

import './WelcomeDirective';
export const WelcomeState: INavigationState = {
    title: 'Welcome',
    name: 'welcome',
    url: '/welcome',
    template: '<welcome></welcome>',
    isDefault: true,
    order: 1
};

angular.module('sogeti-academy')
    .config([
        '$stateProvider',
        '$urlRouterProvider',
        ($stateProvider: angular.ui.IStateProvider,
         $urlRouterProvider: angular.ui.IUrlRouterProvider) => {
            $urlRouterProvider.otherwise(<string>WelcomeState.url);
            $stateProvider.state(WelcomeState);
        }
    ]);