import {PresentationDetailState} from '../../detail/PresentationDetailState';

export class PresentationsController {
    static $inject = ['$mdMedia', '$state'];
    
    get showList(): boolean {
        return this.$mdMedia('gt-sm')
            || !this.$state.is(PresentationDetailState);
    }
    
    get showDetail(): boolean {
        return this.$mdMedia('gt-sm')
            || this.$state.is(PresentationDetailState);
    }
    
    constructor(private $mdMedia: angular.material.IMedia,
        private $state: angular.ui.IStateService) {
        
    }
}