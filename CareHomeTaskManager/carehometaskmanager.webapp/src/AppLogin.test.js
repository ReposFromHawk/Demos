import { shallow, configure } from 'enzyme';
import App from './App';
import Login from './Login';

describe('Login', () => {
  
    it('A Login Form Exists', () => {
        const LoginWrapper = shallow(<Login />);
        const form = LoginWrapper.find('form');
        expect(form).toHaveLength(1);
    });
    it('Login Form Use Post Method', () => {
        const LoginWrapper = shallow(<Login />);
        const method = LoginWrapper.find('form').props().method;
        expect(method).toBe("POST");
    });
    
});