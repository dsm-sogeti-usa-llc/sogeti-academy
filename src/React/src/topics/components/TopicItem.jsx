import React from 'react';

export class TopicItem extends React.Component {
    render() {
        return (
            <li className="list-group-item">
                <form className="form-inline" form="role">
                    <button className="btn btn-default">
                        <span className="glyphicon glyphicon-plus"></span>
                    </button>
                    <span>
                        {this.props.topic.name}
                        <span className="badge">{this.props.topic.votes}</span>
                    </span>
                    <div className="collapse">
                        <input className="form-control" type="email" name="email" id="email" required placeholder="Email" />
                        <button className="btn btn-default">
                            <span className="glyphicon glyphicon-ok"></span>
                        </button>
                        <button className="btn btn-default">
                            <span className="glyphicon glyphicon-remove"></span>
                        </button>
                    </div>
                </form>
            </li>
        );
    }
}