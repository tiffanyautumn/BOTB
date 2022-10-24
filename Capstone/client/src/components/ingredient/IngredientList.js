import { useEffect, useState } from "react"
import { useNavigate } from "react-router-dom"
import { Button, Table } from "reactstrap"
import { getAllIngredients } from "../../modules/ingredientManager"
import { Ingredient } from "./Ingredient"
export const IngredientList = ({ isAdmin, isApproved, searchTermState }) => {
    const [ingredients, setIngredients] = useState([])
    const [filteredIngredients, setFiltered] = useState([])
    const navigate = useNavigate()

    const getIngredients = () => {
        getAllIngredients().then(p => setIngredients(p))
    }

    useEffect(() => {
        getIngredients();
    }, []);

    useEffect(() => {
        getSearchedIngredients(searchTermState);
    },
        [searchTermState]
    )

    const getSearchedIngredients = (searchTerm) => {
        fetch(`/api/ingredient/search?q=${searchTerm}`)
            .then(res => res.json())
            .then((p) => {
                setFiltered(p)
            })
    }

    return (
        <div className="container">
            {
                isAdmin
                    ? <Button onClick={() => navigate('/ingredient/create')}>Create an Ingredient</Button>
                    : ""
            }

            <Table>
                <thead>
                    <tr>
                        <th>Name</th>
                        <th>Use</th>
                        <th>Safety</th>
                        <th>Rating</th>
                    </tr>
                </thead>
                <tbody>
                    {filteredIngredients.map((i) => (
                        <Ingredient ingredient={i} key={i.id} />
                    ))}
                </tbody>
            </Table>
            <div className="row justify-content-center">

            </div>
        </div>
    )
}