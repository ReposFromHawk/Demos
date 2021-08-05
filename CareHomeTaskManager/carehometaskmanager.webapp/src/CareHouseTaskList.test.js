import React from 'react';
import { shallow } from 'enzyme'
import CareHouseTaskList from './CareHouseTaskList';

describe('CareHouseTaskList', () => {
    it('renders a ul elementw', () => {
        const taskListWrapper = shallow(<CareHouseTaskList />);
        const taskListULs = taskListWrapper.find('ul');
        expect(taskListULs).toHaveLength(1);
    });
    it('renders no li elements where no care tasks exits', () => {
        const careHouseTasks = [];
        const taskListWrapper = shallow(<CareHouseTaskList CareHouseTasks={careHouseTasks}/>);
        const careTaskListItems = taskListWrapper.find('li');
        expect(careTaskListItems).toHaveLength(0);
    });
    it('renders 1 li elements where only 1 care tasks exits', () => {
        const careHouseTasks = [{id:1,title:'Feeding care plan'}];
        const taskListWrapper = shallow(<CareHouseTaskList CareHouseTasks={careHouseTasks} />);
        const careTaskListItems = taskListWrapper.find('li');
        expect(careTaskListItems).toHaveLength(1);
    });
    it('renders an li element for each care tasks', () => {
        const careHouseTasks = [
            { id: 1, title: 'Feeding care plan' },
            { id: 2, title: 'Personal hygene plan' }  ];
        const taskListWrapper = shallow(<CareHouseTaskList CareHouseTasks={careHouseTasks} />);
        const careTaskListItems = taskListWrapper.find('li');
        expect(careTaskListItems).toHaveLength(2);
    });
    it('renders the title of the task', () => {
        const careHouseTasks = [
            { id: 1, title: 'Feeding care plan' }];
        const taskListWrapper = shallow(<CareHouseTaskList CareHouseTasks={careHouseTasks} />);
        const careTaskListItems = taskListWrapper.find('li');
        expect(taskListWrapper.find('li').text()).toContain(careHouseTasks[0].title);
    });
});

