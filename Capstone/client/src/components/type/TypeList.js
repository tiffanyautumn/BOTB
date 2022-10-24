import { useEffect, useState } from "react"
import { useNavigate } from "react-router-dom"
import { Button, Table } from "reactstrap"
import { getAllTypes } from "../../modules/typeManager"
import { Type } from "./Type"

export const TypeList = ({ isAdmin, isApproved, searchTermState }) => {
    const [types, setTypes] = useState([])
    const navigate = useNavigate()

    const getTypes = () => {
        getAllTypes().then(t => setTypes(t))
    }

    useEffect(() => {
        getTypes();
    }, []);


    return (
        <div className="container">


            <div className="row ">
                <Button onClick={() => navigate('/type/create')}>Create a Type</Button>
                <Table>
                    <thead>
                        <tr>
                            <th>types</th>

                        </tr>
                    </thead>
                    <tbody>
                        {types.map((type) => (
                            <Type type={type} key={type.id} isAdmin={isAdmin} />
                        ))}
                    </tbody>
                </Table>

            </div>

        </div>
    )
}