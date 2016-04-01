import {ConfigService} from '../../../core/ConfigService';
import {PresentationDetailViewModel} from '../models/PresentationDetailViewModel';

export class PresentationDetailService {
    static $inject = ['$http', 'ConfigService']
    
    constructor(private $http: angular.IHttpService,
        private configService: ConfigService) {
                
    }
    
    getDetail(id: string): Promise<PresentationDetailViewModel> {
        return new Promise<PresentationDetailViewModel>((resolve) => {
            this.$http.get<PresentationDetailViewModel>(`${this.configService.apiUrl}/presentations/${id}`).then(
                (response: angular.IHttpPromiseCallbackArg<PresentationDetailViewModel>) => resolve(response.data)
            );
        });
    }
}

angular.module('sogeti-academy')
    .service('PresentationDetailService', PresentationDetailService);