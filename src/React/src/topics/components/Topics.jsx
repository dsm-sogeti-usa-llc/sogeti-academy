import React from 'react';
import {loadTopics} from '../actions/TopicsActions';
import {CreateTopic} from './CreateTopic';
import {TopicsList} from './TopicsList';
import {TopicsStore} from '../stores/TopicsStore';

import '../styles/topics';

const topicsStore = new TopicsStore();
export class Topics extends React.Component {
    constructor(props, context) {
        super(props, context);
        
        this.state = topicsStore.getState();
        topicsStore.addChangeListener(this.onChange.bind(this));
        loadTopics();
    }
    
    onChange() {
        this.setState(topicsStore.getState());
    }
    
    render() {
        return (
            <div className="topics">
                <div className="row">
                    <div className="col-sm-12">
                        <CreateTopic />
                    </div>
                </div>
                <div className="row">
                    <div className="col-sm-12">
                        <TopicsList topics={this.state.topics} />
                    </div>
                </div>
            </div>
        )
    }
}