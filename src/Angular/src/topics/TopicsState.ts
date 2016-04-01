import {INavigationState} from '../core/INavigationState';

import './TopicsDirective';
export const TopicsState: INavigationState = {
    name: 'topics',
    url: '/topics',
    template: '<topics></topics>',
    title: 'Topics',
    order: 2
}

angular.module('sogeti-academy')
    .config([
        '$stateProvider',
        ($stateProvider: angular.ui.IStateProvider) => {
            $stateProvider.state(TopicsState);
        }
    ])