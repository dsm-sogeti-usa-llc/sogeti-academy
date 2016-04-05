import {INavigationState} from '../../core/INavigationState';

export class ToolbarController {
    static $inject = ['$state', '$mdMedia', '$mdSidenav'];
    
    get title(): string {
        return (<INavigationState>this.$state.current).title
    }
    
    get showNavToggle(): boolean {
        return !this.$mdMedia('gt-md');
    }
    
    constructor(private $state: angular.ui.IStateService,
        private $mdMedia: angular.material.IMedia,
        private $mdSidenav: angular.material.ISidenavService) {
        
    }
    
    toggleNav(): void {
        this.$mdSidenav('left').toggle();
    }
}