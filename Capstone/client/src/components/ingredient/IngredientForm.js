import { useEffect, useState } from "react"
import { useNavigate } from "react-router-dom"
import { Button, Card, CardBody, FormGroup, Input, Label, Form } from "reactstrap"
import { addIngredient } from "../../modules/ingredientManager"

export const IngredientForm = () => {
    const navigate = useNavigate()
    const [ingredient, updateIngredient] = useState({
        name: "",
        function: "",
        safetyInfo: ""
    })



    const handleSaveButtonClick = (event) => {
        event.preventDefault()

        if (ingredient.function === "") {
            ingredient.function = null
        }
        if (ingredient.safetyInfo === "") {
            ingredient.safetyInfo = null
        }

        const ingredientToSend = {
            name: ingredient.name,
            function: ingredient.function,
            safetyInfo: ingredient.safetyInfo
        }

        return addIngredient(ingredientToSend)
            .then(() => {
                navigate("/ingredient")
            })
    }
    return (<>
        <Card>
            <CardBody>

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
                        <Label for="function">Function</Label>
                        <Input
                            id="function"
                            name="function"
                            type="text"
                            value={ingredient.function}
                            onChange={
                                (evt) => {
                                    const copy = { ...ingredient }
                                    copy.function = evt.target.value
                                    updateIngredient(copy)
                                }
                            } />
                    </FormGroup>
                    <FormGroup>
                        <Label for="safety">Safety Information</Label>
                        <Input
                            id="safety"
                            name="safety"
                            type="text"
                            value={ingredient.safetyInfo}
                            onChange={
                                (evt) => {
                                    const copy = { ...ingredient }
                                    copy.safetyInfo = evt.target.value
                                    updateIngredient(copy)
                                }
                            } />
                    </FormGroup>


                    <Button onClick={(clickEvent) => handleSaveButtonClick(clickEvent)}
                        className="btn btn-primary">
                        Save
                    </Button>
                </Form>
                <Button onClick={() => navigate("/ingredient")}>Cancel</Button>
            </CardBody>
        </Card>
    </>)
}