import {INavigationState} from '../../core/INavigationState';

function map(obj: Object): INavigationState {
    for(let prop in obj) {
        if(obj.hasOwnProperty(prop)) {
            return obj[prop];
        }
    }
    
    return obj;
}
const context = (<any>require).context('../..', true, /State$/);
const states = context.keys().map(context)
    .map(map)
    .filter(s => s !== undefined)
    .filter(s => s.title !== undefined)
    .filter(s => s.parent === undefined);
export class SidenavService {
    getAllStates(): INavigationState[] {
        return states.quickSort(s => s.order);
    }
}

angular.module('sogeti-academy')
    .service('SidenavService', SidenavService);