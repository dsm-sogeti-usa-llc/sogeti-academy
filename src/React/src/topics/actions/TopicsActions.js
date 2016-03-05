import {AppDispatcher} from '../../core/AppDispatcher';
import {TopicsService} from '../services/TopicsService';

export const LOAD_TOPICS_SUCCESS = 'LOAD_TOPICS_SUCCESS';

export function createTopic(name) {
    const action = {
        actionType: 'CREATE_TOPIC',
        data: {
            topicName: name
        }
    };

    AppDispatcher.dispatch(action);
    TopicsService.createTopic(name)
        .then(
            id => createTopicSuccess(id, name),
            error => createTopicFailed(error)
        );
}

export function createTopicSuccess(id, name) {
    const action = {
        actionType: 'CREATE_TOPIC_SUCCESS',
        data: {
            id: id,
            name: name,
            votes: 0
        }
    };
    AppDispatcher.dispatch()
}

export function createTopicFailed(error) {
    const action = {
        actionType: 'CREATE_TOPIC_FAILED',
        data: {
            error: error
        }
    };
    AppDispatcher.dispatch(action);
}

export function voteForTopic(topicId, email) {
    const action = {
        actionType: 'VOTE_FOR_TOPIC',
        data: {
            topicId: topicId,
            email: email
        }
    };
    AppDispatcher.dispatch(action);
    TopicsService.voteForTopic(topicId, email)
        .then(
            () => voteForTopicSuccess(topicId),
            error => voteForTopicFailed(error) 
        );
}

export function voteForTopicSuccess(topicId) {
    const action = {
        actionType: 'VOTE_FOR_TOPIC_SUCCESS',
        data: {
            topicId: topicId
        }
    };
    AppDispatcher.dispatch(action);
}

export function voteForTopicFailed(error) {
    const action = {
        actionType: 'VOTE_FOR_TOPIC_FAILED',
        data: {
            error: error
        }
    };
    AppDispatcher.dispatch(action);
}

export function loadTopics() {
    const action = {
        actionType: 'LOAD_TOPICS',
        data: {}
    };
    AppDispatcher.dispatch(action);

    TopicsService.getTopics().then(
        (topics) => topicsLoadSuccess(topics),
        (error) => topicsLoadFailed(error)
        );
}

export function topicsLoadSuccess(topics) {
    const action = {
        actionType: LOAD_TOPICS_SUCCESS,
        data: {
            topics: topics
        }
    };
    AppDispatcher.dispatch(action);
}

export function topicsLoadFailed(error) {
    const action = {
        actionType: 'LOAD_TOPICS_FAILED',
        data: {
            statusText: error
        }
    };
    AppDispatcher.dispatch(action);
}