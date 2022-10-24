import { useEffect, useState } from "react"
import { useNavigate } from "react-router-dom"
import { Button, Card, CardBody, FormGroup, Input, Label, Form } from "reactstrap"
import { getAllIngredients } from "../../modules/ingredientManager"
import { addProductIngredient } from "../../modules/productIngredientManager"

export const ProductIngredientForm = ({ product, setFormActive, getProduct }) => {
    const navigate = useNavigate()
    const [ingredients, setIngredients] = useState([])
    const [productIngredient, updateProductIngredient] = useState({
        ingredientId: 0,
        active: false,
        use: ""
    })

    const getIngredients = () => {
        getAllIngredients().then(p => setIngredients(p))
    }

    useEffect(() => {
        getIngredients();
    }, []);



    const handleSaveButtonClick = (event) => {
        event.preventDefault()

        if (productIngredient.use === "") {
            productIngredient.use = null
        }


        const productIngredientToSend = {
            ingredientId: parseInt(productIngredient.ingredientId),
            productId: product.id,
            active: productIngredient.active,
            use: productIngredient.use
        }

        return addProductIngredient(productIngredientToSend)
            .then(() => {
                setFormActive(false)
                getProduct()
            })
    }
    return (<>
        <Card>
            <CardBody>
                <FormGroup>
                    <Label for="ingredientSelect">Ingredient</Label>
                    <Input
                        id="ingredientSelect"
                        name="ingredientId"
                        type="select"
                        onChange={
                            (evt) => {
                                const copy = { ...productIngredient }
                                copy.ingredientId = evt.target.value
                                updateProductIngredient(copy)
                            }
                        }>
                        <option>choose an ingredient</option>
                        {ingredients.map((i) => (
                            <option value={i.id} key={i.id}>{i.name}</option>
                        ))}
                    </Input>
                </FormGroup>
                <Form className="ingredientForm">
                    <FormGroup>
                        <Label for="active">Active</Label>
                        <Input
                            id="active"
                            name="active"
                            type="checkbox"
                            value={productIngredient.active}
                            onChange={
                                (evt) => {
                                    const copy = { ...productIngredient }
                                    copy.active = evt.target.checked
                                    updateProductIngredient(copy)
                                }
                            } />
                    </FormGroup>
                    <FormGroup>
                        <Label for="use">Use</Label>
                        <Input
                            id="use"
                            name="use"
                            type="text"
                            value={productIngredient.use}
                            onChange={
                                (evt) => {
                                    const copy = { ...productIngredient }
                                    copy.use = evt.target.value
                                    updateProductIngredient(copy)
                                }
                            } />
                    </FormGroup>

                    <Button onClick={(clickEvent) => handleSaveButtonClick(clickEvent)}
                        className="btn btn-primary">
                        Save
                    </Button>
                </Form>
                <Button onClick={() => setFormActive(false)}>Cancel</Button>
            </CardBody>
        </Card>
    </>)
}