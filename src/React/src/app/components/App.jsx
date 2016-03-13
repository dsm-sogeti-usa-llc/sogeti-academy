import React from 'react';
import {Navbar} from './Navbar';
import {Loading} from '../../loading/components/Loading';

export class App extends React.Component {
    render() {
        return (
            <div>
                <Navbar />
                <div className="container">
                    <div className="row">
                        <div className="col-sm-12">
                            {this.props.children}
                        </div>
                    </div>
                </div>
                <Loading />
            </div>
        );
    }
} 