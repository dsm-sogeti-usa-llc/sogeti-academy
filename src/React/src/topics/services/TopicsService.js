import * as jQuery from 'jquery';
import {Promise} from 'es6-promise';
import {config} from '../../core/config';

function getTopics() {
    return new Promise((resolve, reject) => {
        jQuery.get(`${config.apiUrl}/topics`)
            .success(data => resolve(data.Topics))
            .error((jqXHR, textStatus) => reject(textStatus));
    });
}

function createTopic(name) {
    return new Promise((resolve, reject) => {
        const data = { name: name };
        jQuery.post(`${config.apiUrl}/topics`, data)
            .success(data => resolve(data))
            .error((jqXHR, textStatus) => reject(textStatus));
    });
}

function voteForTopic(topicId, email) {
    return new Promise((resolve, reject) => {
        const data = { topicId: topicId, email: email };
        jQuery.post(`${config.apiUrl}/topics/${topicId}/vote`, data)
            .success(data => resolve(data))
            .error((jqXHR, textStatus) => reject(textStatus));
    });
}

const TopicsService = {
    getTopics: getTopics,
    createTopic: createTopic,
    voteForTopic: voteForTopic
};

export {TopicsService};