import {SidenavService} from '../services/SidenavService';
import {INavigationState} from '../../core/INavigationState';

import '../services/SidenavService';
export class SidenavController {
    static $inject = ['$mdMedia', '$state', 'SidenavService'];
    
    isSidenavOpen: boolean;
    get isDockedOpen(): boolean {
        return this.$mdMedia('gt-md');
    }
    
    get states(): INavigationState[] {
        return this.SidenavService.getAllStates();
    }
    
    get defaultState(): INavigationState {
        return this.states.filter(s => s.isDefault)[0];
    }
    constructor(private $mdMedia: angular.material.IMedia,
        private $state: angular.ui.IStateService,
        private SidenavService: SidenavService) {
        
    }
    
    navigate(state: INavigationState): void {
        this.$state.go(state);
        this.isSidenavOpen = false;
    }
}