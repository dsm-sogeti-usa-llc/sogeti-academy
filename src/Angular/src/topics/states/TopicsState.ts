import {INavigationState} from '../../core/INavigationState';

import '../TopicsDirective';
export const TopicsState: INavigationState = {
    name: 'topics',
    url: '/topics',
    template: '<topics></topics>',
    title: 'Topics'
}

angular.module('sogeti-academy')
    .config([
        '$stateProvider',
        ($stateProvider: angular.ui.IStateProvider) => {
            $stateProvider.state(TopicsState);
        }
    ])