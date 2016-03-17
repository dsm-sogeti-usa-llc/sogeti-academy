import {Topic} from '../models/Topic';
import {Vote} from '../models/Vote';
import {ConfigService} from '../../core/ConfigService';

import '../../core/ConfigService';
export class TopicsService {
    static $inject = ['$http', 'ConfigService'];
    private _apiUrl: string;
    constructor(private $http: angular.IHttpService,
        private configService: ConfigService) {
        this._apiUrl = configService.apiUrl;
    }
    
    getAll(): Promise<Topic[]> {
        return new Promise<Topic[]>((resolve) => {
            const url = `${this._apiUrl}/topics`;
            this.$http.get(url)
                .then(
                    (response: angular.IHttpPromiseCallbackArg<any>) => resolve(response.data.Topics)
                );
        });
    }
    
    createTopic(topic: Topic): Promise<string> {
        throw 'Not Implemented';
    }
    
    voteForTopic(vote: Vote): Promise<void> {
        throw 'Not Implemented';
    }
}
angular.module('sogeti-academy')
    .service('TopicsService', TopicsService);