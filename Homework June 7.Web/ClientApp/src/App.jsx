import React, { useState } from 'react';
import axios from 'axios';

const App = () => {

    const [items, setItems] = useState([]);

    const onSearchClick = async () => {
        const { data } = await axios.get(`/api/TLS/scrape`);
        setItems(data);
    }

    return (
        <div className='container mt-5'>

            <div class="d-grid gap-2">
                <button onClick={onSearchClick} className='btn btn-primary'>Search TLS news</button>
            </div>
            <div className='row mt-3'>
                <table className='table table-bordered'>
                    <thead>
                        <tr>
                            <th>Image</th>
                            <th>Title</th>
                        </tr>
                    </thead>
                    <tbody>
                        {items.map(item => {
                            return <tr key={item.url}>
                                <td><img src={item.image} style={{ width: 200 }} /></td>
                                <td>
                                    <a target="_blank" href={item.url}>{item.title}</a>
                                    <br></br>
                                    <div class="d-grid gap-2 d-md-flex justify-content-md-end">
                                        <button class="btn btn-outline-primary" type="button" disabled>{item.comments} comments</button>
                                    </div>
                                </td>
                            </tr>
                        })}
                    </tbody>
                </table>
            </div>
        </div>
    )

}

export default App;