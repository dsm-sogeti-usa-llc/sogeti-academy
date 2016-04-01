import {INavigationState} from '../../core/INavigationState';

export const PresentationDetailState: INavigationState = {
    name: 'presentations.detail',
    template: '<presentation-detail></presentation-detail>',
    url: '/presentations/:id',
    title: 'Presentation Detail',
    parent: 'presentations'
};

angular.module('sogeti-academy')
    .config([
        '$stateProvider',
        ($stateProvider: angular.ui.IStateProvider) => {
            $stateProvider.state(PresentationDetailState);
        }
    ])