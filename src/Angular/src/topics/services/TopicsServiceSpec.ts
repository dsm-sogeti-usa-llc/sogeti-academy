import {TopicsService} from './TopicsService';
import {ConfigService} from '../../core/ConfigService';
import {Topic} from '../models/Topic';
import {Vote} from '../models/Vote';

describe('TopicsService', () => {
    let $httpBackend: angular.IHttpBackendService;
    let topicsService: TopicsService;
    let configService: ConfigService;

    beforeEach(angular.mock.module('sogeti-academy'));

    beforeEach(angular.mock.inject((_$httpBackend_, _ConfigService_, _TopicsService_) => {
        $httpBackend = _$httpBackend_;
        topicsService = _TopicsService_;
        configService = _ConfigService_;
    }));

    it('should get topics from api', (done) => {
        const url = `${configService.apiUrl}/topics`;
        const topics: Topic[] = [
            { name: 'one', votes: 1 },
            { name: 'two', votes: 0 },
            { name: 'three', votes: -1 }
        ];
        $httpBackend.expectGET(url).respond({
            topics: topics
        });
        
        topicsService.getAll().then((actual: Topic[]) => {
            expect(actual).toEqual(topics);
            done();
        });
        $httpBackend.flush();
    });

    it('should create topic in api', (done) => {
        const url = `${configService.apiUrl}/topics`;
        const id = 'gasdfsad;flk';
        const topic: Topic = {name: 'something', votes: 0 };
        
        $httpBackend.expectPOST(url, topic).respond(id);
        
        topicsService.createTopic(topic).then(actual => {
            expect(actual).toBe(id);
            done();   
        });
        $httpBackend.flush();
    });
    
    it('should vote in api', (done) => {
        const vote: Vote = { topicId: 'asdgasdf', email: 'asdgadsfsf' };
        const url = `${configService.apiUrl}/topics/${vote.topicId}/vote`;
         $httpBackend.expectPOST(url, vote).respond({});
         
         topicsService.voteForTopic(vote).then(() =>{
             done();
         });
         $httpBackend.flush();
    });

    afterEach(() => {
        $httpBackend.verifyNoOutstandingExpectation();
        $httpBackend.verifyNoOutstandingRequest();
    });
});