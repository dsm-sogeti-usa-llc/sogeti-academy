import {Presentation} from '../models/Presentation';
import {ConfigService} from '../../../core/ConfigService';

export class PresentationListService {
    static $inject = ['$http', 'ConfigService'];
    
    constructor(private $http: angular.IHttpService,
        private configService: ConfigService) {
        
    }
    
    getPresentations(): Promise<Presentation[]> {
        return new Promise<Presentation[]>((resolve) => {
             const url = `${this.configService.apiUrl}/presentations`;
             this.$http.get(url)
                .then(
                    (response: angular.IHttpPromiseCallbackArg<any>) => resolve(response.data.presentations)
                )
        });
    }
}

angular.module('sogeti-academy')
    .service('PresentationListService', PresentationListService);