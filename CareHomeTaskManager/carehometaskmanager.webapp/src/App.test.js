import { shallow } from 'enzyme';
import App from './App';
import { configure } from 'enzyme';
import CareHouseTaskList from './CareHouseTaskList';
import Login from './Login';

describe('App', () => {
 let appWrapper;

    beforeAll( () => {
        appWrapper = shallow(<App />);
    });
   

    it('renders a care home task list', () => {        
        const careTaskList = appWrapper.find(CareHouseTaskList);
        expect(careTaskList).toHaveLength(1);
    });

    it('App should have a state', () => {       
        const appState = appWrapper.state();
        expect(appState).not.toBeNull();
    });

    it('Has A CareHouseTasks Property on state', () => {       
        const appState = appWrapper.state();
        expect(appState.CareHouseTasks).toBeDefined();
    });

    it('passes careHouseTasks property of state to careHouseTasks as prop', () => {
        const careTaskList = appWrapper.find(CareHouseTaskList);
        expect(careTaskList.props().CareHouseTasks).toEqual(appWrapper.state().CareHouseTasks);
    });

    it('', () => {
        const loginSection = appWrapper.find(Login);
        expect(loginSection).toHaveLength(1);
    });
});
