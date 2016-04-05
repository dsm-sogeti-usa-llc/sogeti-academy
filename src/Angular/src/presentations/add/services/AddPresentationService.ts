import {AddPresentationViewModel} from '../models/AddPresentationViewModel';
import {ConfigService} from '../../../core/ConfigService';

export class AddPresentationService {
    static $inject = ['Upload', 'ConfigService'];
    
    constructor(private upload: angular.angularFileUpload.IUploadService,
        private configService: ConfigService) {
            
    }
    
    save(viewModel: AddPresentationViewModel): Promise<string> {
        return new Promise<string>((resolve) => {
            const url = `${this.configService.apiUrl}/presentations`;
            const json = this.upload.json(viewModel);
            this.upload.upload<string>({
                data: viewModel,
                url: url,
                method: 'POST'
            }).then(
                (response: angular.IHttpPromiseCallbackArg<string>) => resolve(response.data)
            );
        });
    }
}

angular.module('sogeti-academy')
    .service('AddPresentationService', AddPresentationService);