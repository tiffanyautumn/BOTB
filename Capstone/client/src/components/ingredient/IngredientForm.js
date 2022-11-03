import { useEffect, useState } from "react"
import { useNavigate } from "react-router-dom"
import { Button, Card, CardBody, FormGroup, Input, Label, Form } from "reactstrap"
import { addIngredient } from "../../modules/ingredientManager"

export const IngredientForm = () => {
    const navigate = useNavigate()
    const [ingredient, updateIngredient] = useState({
        name: "",
        imageUrl: ""
    })



    const handleSaveButtonClick = (event) => {
        event.preventDefault()


        if (ingredient.imageUrl === "") {
            ingredient.imageUrl = null
        }

        const ingredientToSend = {
            name: ingredient.name,
            imageUrl: ingredient.imageUrl
        }

        return addIngredient(ingredientToSend)
            .then(() => {
                navigate("/ingredient")
            })
    }
    return (<>

        <div className="ingredientform">
            <h3>Create a new Ingredient</h3>

            <Form className="ingredientForm">
                <FormGroup>
                    <Label for="name">Ingredient</Label>
                    <Input
                        id="name"
                        name="name"
                        type="text"
                        value={ingredient.name}
                        onChange={
                            (evt) => {
                                const copy = { ...ingredient }
                                copy.name = evt.target.value
                                updateIngredient(copy)
                            }
                        } />
                </FormGroup>
                <FormGroup>
                    <Label for="function">Image Link</Label>
                    <Input
                        id="function"
                        name="function"
                        type="text"
                        value={ingredient.imageUrl}
                        onChange={
                            (evt) => {
                                const copy = { ...ingredient }
                                copy.imageUrl = evt.target.value
                                updateIngredient(copy)
                            }
                        } />
                </FormGroup>



                <Button onClick={(clickEvent) => handleSaveButtonClick(clickEvent)}
                    className="btn btn-primary">
                    Save
                </Button>
                <Button onClick={() => navigate("/ingredient")}>Cancel</Button>
            </Form>


        </div>
    </>)
}