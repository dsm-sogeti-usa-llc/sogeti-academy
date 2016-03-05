import React from 'react';
import {TopicItem} from './TopicItem';

export class TopicsList extends React.Component {
    render() {
        const items = this.props.topics.map(t => <TopicItem key={t.Id} topic={t} />);
        return (
            <ul className="list-group topics-list">
                {items}
            </ul>  
        );
    }
}