import {INavigationState} from '../../core/INavigationState';
import {TopicsState} from '../../topics/TopicsState';

export class WelcomeController {
    get topicsState(): INavigationState {
        return TopicsState;
    }
}