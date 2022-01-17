
import React, { Component } from 'react';

class PictureFit extends Component {
    constructor(props) {
        super(props);

        console.log(this.props)

        this.state = {
            picUrl: this.props.picUrl,
            picFrame: this.props.picFrame,
            width: this.props.picWidth,
            height: this.props.picHeight
        }
    }

    componentDidUpdate(prevProps) {
        if (prevProps.picUrl !== this.props.picUrl ||
            prevProps.picFrame !== this.props.picFrame ||
            prevProps.picWidth !== this.props.picWidth ||
            prevProps.picHeight !== this.props.picHeight) {

          let updatedState = this.state;

          updatedState = {
            picFrame: this.props.picFrame,
            picUrl: this.props.picUrl,
            width: this.props.picWidth,
            height: this.props.picHeight
          }

          this.setState(updatedState);
        
        }
      }

    render() {

      const styleDiv = {
        position: "relative"
      };

      const styleFrame = {
        width: this.state.width + 10,
        height: this.state.height + 10,
        position: "absolute"
      };

      const stylePic = {
        
        width: this.state.width,
        height: this.state.height,
        top: 5,
        left: 5,
        position: "absolute"
      };

      console.log("this.state.picUrl: ", this.state.picUrl)
      return (
            <div style={styleDiv}>
              <img style={styleFrame}
                src={this.state.picFrame}>
              </img>
              <img style={stylePic}
                  src={this.state.picUrl}></img>
            </div>
            
      )
    }
}

export default PictureFit;