import {FileDetailViewModel} from './FileDetailViewModel';

export interface PresentationDetailViewModel {
    id: string;
    topic: string;
    description: string;
    files: FileDetailViewModel[];
}