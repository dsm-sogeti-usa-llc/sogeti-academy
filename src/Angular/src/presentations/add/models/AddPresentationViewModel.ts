import {AddFileViewModel} from './AddFileViewModel';

export interface AddPresentationViewModel {
    topic: string;
    description: string;
    files?: AddFileViewModel[];
}