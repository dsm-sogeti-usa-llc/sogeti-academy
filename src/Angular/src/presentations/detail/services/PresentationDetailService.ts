import {ConfigService} from '../../../core/ConfigService';
import {PresentationDetailViewModel} from '../models/PresentationDetailViewModel';
import {FileDetailViewModel} from '../models/FileDetailViewModel';

export class PresentationDetailService {
    static $inject = ['$http', '$window', 'Upload', 'ConfigService']
    
    constructor(private $http: angular.IHttpService,
        private $window: angular.IWindowService,
        private upload: angular.angularFileUpload.IUploadService,
        private configService: ConfigService) {
                
    }
    
    getDetail(id: string): Promise<PresentationDetailViewModel> {
        return new Promise<PresentationDetailViewModel>((resolve) => {
            this.$http.get<PresentationDetailViewModel>(`${this.configService.apiUrl}/presentations/${id}`).then(
                (response: angular.IHttpPromiseCallbackArg<PresentationDetailViewModel>) => resolve(response.data)
            );
        });
    }
    
    save(viewModel: PresentationDetailViewModel): Promise<void> {
        return new Promise<void>((resolve) => {
            this.upload.upload<void>({
                data: viewModel,
                method: 'PUT',
                url: `${this.configService.apiUrl}/presentations/${viewModel.id}`
            }).then(
                (response: angular.IHttpPromiseCallbackArg<void>) => resolve()
            );
        });
    }
    
    downloadFile(file: FileDetailViewModel, presentation: PresentationDetailViewModel): Promise<void> {
        return new Promise<void>((resolve) => {
            this.$window.open(`${this.configService.apiUrl}/presentations/${presentation.id}/files/${file.id}`, 'blank');
            resolve();
        });
    }
}

angular.module('sogeti-academy')
    .service('PresentationDetailService', PresentationDetailService);