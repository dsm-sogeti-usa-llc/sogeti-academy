import {AddPresentationViewModel} from '../models/AddPresentationViewModel';

export class AddPresentationService {
    save(viewModel: AddPresentationViewModel): Promise<string> {
        throw 'not implemented';
    }
}

angular.module('sogeti-academy')
    .service('AddPresentationService', AddPresentationService);