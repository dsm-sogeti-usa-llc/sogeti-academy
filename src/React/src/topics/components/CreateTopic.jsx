import React from 'react';

export class CreateTopic extends React.Component {
    render() {
        return (
            <form className="form-inline" role="form">
                <button className="btn btn-primary">
                    Create Topic
                </button>
                <div className="collapse create-topic-form">
                    <input className="form-control" type="text" name="name" id="name" required placeholder="Topic" />
                    <button className="btn btn-default">
                        <span className="glyphicon glyphicon-ok"></span>
                    </button>
                    <button className="btn btn-default">
                        <span className="glyphicon glyphicon-remove"></span>
                    </button>
                </div>
            </form>  
        );
    }
}