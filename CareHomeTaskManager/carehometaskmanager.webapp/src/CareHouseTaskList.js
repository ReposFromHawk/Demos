import React from 'react';

function CareHouseTaskList({ CareHouseTasks=[] }) {
    return <ul>{CareHouseTasks.map((x, i) => <li key={i}>{x.title}</li> )}</ul>;
};
export default CareHouseTaskList;