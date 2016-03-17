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
        return new Promise<string>((resolve) => {
           const url = `${this._apiUrl}/topics`;
           this.$http.post(url, topic)
                .then(
                    (response: angular.IHttpPromiseCallbackArg<any>) => resolve(response.data)
                );
        });
    }
    
    voteForTopic(vote: Vote): Promise<void> {
        return new Promise<void>((resolve) => {
            const url = `${this._apiUrl}/topics/${vote.topicId}/vote`;
            this.$http.post(url, vote)
                .then(
                    (response: angular.IHttpPromiseCallback<any>) => resolve()
                );
        });
    }
}
angular.module('sogeti-academy')
    .service('TopicsService', TopicsService);