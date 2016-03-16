import {SideNavService} from '../services/SideNavService';
import {INavigationState} from '../../core/INavigationState';

import '../services/SideNavService';
export class SideNavController {
    static $inject = ['$mdMedia', 'SideNavService'];
    
    get isDockedOpen(): boolean {
        return this.$mdMedia('gt-md');
    }
    
    get states(): INavigationState[] {
        return this.SideNavService.getAllStates();
    }
    
    get defaultState(): INavigationState {
        return this.states.filter(s => s.isDefault)[0];
    }
    constructor(private $mdMedia: angular.material.IMedia,
        private SideNavService: SideNavService) {
        
    }
}