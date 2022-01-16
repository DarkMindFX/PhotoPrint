
import React, { Component } from 'react';

class PictureFit extends Component {
    constructor(props) {
        super(props);

        console.log(this.props)

        this.state = {
            picUrl: this.props.picUrl,
            width: this.props.picWidth,
            height: this.props.picHeight
        }
    }

    componentDidUpdate(prevProps) {
        if (prevProps.picUrl !== this.props.picUrl ||
            prevProps.picWidth !== this.props.picWidth ||
            prevProps.picHeight !== this.props.picHeight) {

          let updatedState = this.state;

          updatedState = {
            picUrl: this.props.picUrl,
            width: this.props.picWidth,
            height: this.props.picHeight
          }

          this.setState(updatedState);
        
        }
      }

    render() {
        console.log("this.state.picUrl: ", this.state.picUrl)
        return (
            <img    src={this.state.picUrl} 
                    width={this.state.width}
                    height={this.state.height}
            ></img>
        )
    }
}

export default PictureFit;