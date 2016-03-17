import {INavigationState} from '../../core/INavigationState';
import {TopicsState} from '../../topics/states/TopicsState';

export class WelcomeController {
    get topicsState(): INavigationState {
        return TopicsState;
    }
}