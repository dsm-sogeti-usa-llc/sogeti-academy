import {INavigationState} from '../../core/INavigationState';
import {SidenavService} from './SidenavService';
import {WelcomeState} from '../../welcome/WelcomeState';
import {TopicsState} from '../../topics/TopicsState';
import {PresentationsState} from '../../presentations/PresentationsState';

describe('SideNavService', () => {
    let sidenavService: SidenavService

    beforeEach(angular.mock.module('sogeti-academy'));

    beforeEach(angular.mock.inject((_SidenavService_) => {
        sidenavService = _SidenavService_;
    }))

    it('should include WelcomeState', () => {
        const states = sidenavService.getAllStates();
        expect(states.indexOf(WelcomeState)).toBeGreaterThan(-1);
    });

    it('should include TopicsState', () => {
        const states = sidenavService.getAllStates();
        expect(states.indexOf(TopicsState)).toBeGreaterThan(-1);
    });

    it('should include PresentationsState', () => {
        const states = sidenavService.getAllStates();
        expect(states.indexOf(PresentationsState)).toBeGreaterThan(-1);
    });

    it('should only include top level navigation states', () => {
        const expected = getStates();
        const actual = sidenavService.getAllStates();
        expect(actual.length).toBe(actual.length);
    });

    it('should order states', () => {
        const expected = getStates().quickSort(s => s.order);
        const actual = sidenavService.getAllStates();
        for (var i = 0; i < expected.length; i++)
            expect(expected[i]).toEqual(actual[i]);
    });

    function getStates(): INavigationState[] {
        const context = (<any>require).context('../..', true, /State$/);
        return context.keys().map(context).map(map)
            .filter(s => s !== undefined)
            .filter(s => s.title !== undefined)
            .filter(s => s.parent === undefined);
    }

    function map(obj: Object): INavigationState {
        for (let prop in obj) {
            if (obj.hasOwnProperty(prop)) {
                return obj[prop];
            }
        }
        return obj;
    }
});