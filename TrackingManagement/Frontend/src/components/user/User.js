import {
  Table,
  Input,
  Button,
  Select,
  Drawer,
  Form,
  notification,
  Modal,
  Space,
  Tag,
  Typography,
  Popconfirm,
} from "antd"
import { CheckOutlined } from "@ant-design/icons"
import { useEffect, useState } from "react"

import makeRequest from "../../utils/makeRequest"
import { RECORD_MODE } from "../../const/mode"
import { addListKey } from "../../utils/addListKey"
import omitNil from "../../utils/omit"
import { requestUrl } from "../../resource/requestUrl"
import SearchText from "../seachItem/SearchText"
import CustomSkeleton from "../skeleton/CustomSkeleton"

const { Option } = Select
const { Text } = Typography

const User = () => {
  const [users, setUsers] = useState([])
  const [units, setUnits] = useState([])
  const [scopes, setScopes] = useState([])
  const [visible, setVisible] = useState(false)
  const [modalVisible, setModalVisible] = useState(false)
  const [mode, setMode] = useState(RECORD_MODE.CREATE)
  const [selectedRecord, setSelectedRecord] = useState({})
  const [pagination, setPagination] = useState({
    current: 1,
    pageSize: 10,
    total: 0,
    showTotal: (total, range) => `${range[0]}-${range[1]} of ${total}`,
  })
  const [filter, setFilter] = useState({
    userName: "",
  })
  const [lastFilter, setLastFilter] = useState({
    userName: "",
  })
  const [loading, setLoading] = useState(true)

  const [form] = Form.useForm()
  const [modalForm] = Form.useForm()

  const handleSearch = async (params, pagination = {}) => {
    setLoading(true)
    let filter = omitNil(params)
    makeRequest({
      method: "GET",
      url: requestUrl.user.readUrl(),
      params: {
        ...filter,
        page: pagination.current,
        record: pagination.pageSize,
      },
    }).then((searchRs) => {
      setUsers(addListKey(searchRs.data))
      setPagination({
        ...pagination,
        total: searchRs.totalRecords,
      })
      setLoading(false)
    })
  }

  const handlePaginationChange = (page, pageSize) => {
    let newPagination = { ...pagination, current: page, pageSize }
    handleSearch(filter, newPagination)
  }

  const onClose = () => {
    setSelectedRecord({})
    setVisible(false)
  }

  const openCreateForm = () => {
    setVisible(true)
    setMode(RECORD_MODE.CREATE)
    setSelectedRecord({})
    form.resetFields()
    form.setFieldsValue({})
  }

  const onBtnUpdateClick = (record) => {
    setVisible(true)
    const selectedRecord = {
      id: record.id,
      userName: record.userName,
      password: record.password,
      scopeId: record.scopeId,
      unitIds: record.userUnits.map((item) => item.unitId),
    }
    setSelectedRecord(selectedRecord)
    form.resetFields()
    form.setFieldsValue(selectedRecord)
    setMode(RECORD_MODE.UPDATE)
  }

  const onChangePassword = (record) => {
    const selectedRecord = {
      id: record.id,
      userName: record.userName,
      password: "",
      scopeId: record.scopeId,
      unitIds: record.userUnits.map((item) => item.unitId),
    }
    setSelectedRecord(selectedRecord)
    modalForm.resetFields()
    modalForm.setFieldsValue(selectedRecord)
    setModalVisible(true)
  }

  const onSavePassword = (account) => {
    makeRequest({
      method: "PUT",
      url: requestUrl.user.updateUrl({ id: account.id }),
      data: account,
    }).then((rs) => {
      notification.open({
        message: "Th??ng b??o",
        icon: <CheckOutlined style={{ color: "#2fd351" }} />,
        description: "?????i m???t kh???u th??nh c??ng",
      })
      setSelectedRecord({})
      setModalVisible(false)
    })
  }

  const onDelUser = (userId) => {
    makeRequest({
      method: "DELETE",
      url: requestUrl.user.deleteUrl(userId),
    }).then((rs) => {
      notification.open({
        message: "Th??ng b??o",
        icon: <CheckOutlined style={{ color: "#2fd351" }} />,
        description: "Xo?? t??i kho???n th??nh c??ng",
      })
      handleSearch(filter, { ...pagination, current: 1 })
    })
  }

  const onFinish = (account) => {
    setLoading(true)
    let method = mode === RECORD_MODE.CREATE ? "POST" : "PUT"
    let url =
      mode === RECORD_MODE.CREATE
        ? requestUrl.user.createUrl()
        : requestUrl.user.updateUrl({ id: account.id })
    let msg =
      mode === RECORD_MODE.CREATE
        ? "Th??m xe m???i th??nh c??ng"
        : "C???p nh???t th??nh c??ng"
    makeRequest({
      method,
      url,
      data: account,
    }).then((rs) => {
      notification.open({
        message: "Th??ng b??o",
        icon: <CheckOutlined style={{ color: "#2fd351" }} />,
        description: msg,
      })
      handleSearch(filter, pagination)
      setSelectedRecord({})
      setVisible(false)
      setLoading(false)
    })
  }

  const columns = [
    {
      title: "T??n ????ng nh???p",
      dataIndex: "userName",
      width: 250,
      align: "center",
    },
    {
      title: "Nh??m quy???n",
      render: (text, record) => <>{record.scope.name}</>,
      width: 200,
      align: "center",
    },
    {
      title: "????n v??? qu???n l??",
      render: (text, record) => (
        <Space>
          {record.userUnits
            .map((userUnit) => {
              return units.filter((u) => u.id === userUnit.unitId)[0]
            })
            .map((unit, index) => {
              return (
                <Tag color="orange" key={index}>
                  {unit.name}
                </Tag>
              )
            })}
        </Space>
      ),
    },
    {
      title: "H??nh ?????ng",
      render: (text, record) => (
        <Space>
          <Button
            type="primary"
            onClick={() => onBtnUpdateClick(record)}
            size="small"
          >
            S???a th??ng tin
          </Button>
          <Button
            type="primary"
            onClick={() => onChangePassword(record)}
            size="small"
          >
            ?????i m???t kh???u
          </Button>
          <Popconfirm
            title="Ch???c ch???n mu???n xo?? t??i kho???n"
            onConfirm={() => onDelUser(record.id)}
            okText="Xo??"
            cancelText="Hu???"
            placement="topRight"
          >
            <Button type="primary" danger size="small">
              Xo??
            </Button>
          </Popconfirm>
        </Space>
      ),
      width: 320,
      align: "center",
    },
  ]

  useEffect(() => {
    const getData = async () => {
      let [userRs, unitRs, scopeRs] = await Promise.all([
        makeRequest({
          method: "GET",
          url: requestUrl.user.readUrl(),
        }),
        makeRequest({
          method: "GET",
          url: requestUrl.unit.readUrl(),
          params: {
            paging: false,
          },
        }),
        makeRequest({
          method: "GET",
          url: requestUrl.scope.readUrl(),
        }),
      ])
      setUnits(addListKey(unitRs.data))
      setScopes(addListKey(scopeRs))
      setUsers(addListKey(userRs.data))
      setPagination({ ...pagination, total: userRs.totalRecords })
      setLoading(false)
    }

    getData()
  }, [])

  return (
    <>
      <div style={{ display: "flex", justifyContent: "space-between" }}>
        <h2>Danh s??ch t??i kho???n</h2>
        <Button type="primary" onClick={() => openCreateForm()}>
          Th??m t??i kho???n m???i
        </Button>
      </div>
      <Space style={{ marginBottom: 10 }}>
        <SearchText
          dataIndex="userName"
          filter={filter}
          pagination={pagination}
          handleSearch={handleSearch}
          placeholder={"Nh???p t??n t??i kho???n"}
          setFilter={setFilter}
          setLastFilter={setLastFilter}
          lastFilter={lastFilter}
        ></SearchText>
      </Space>
      <Table
        dataSource={users}
        columns={columns}
        size="small"
        pagination={{
          position: ["bottomRight"],
          ...pagination,
          onChange: handlePaginationChange,
        }}
      />
      <Drawer
        title={mode === RECORD_MODE.CREATE ? "Th??m xe m???i" : "C???p nh???t"}
        placement="right"
        onClose={onClose}
        visible={visible}
        width="500"
      >
        <Form
          name="accountForm"
          labelCol={{ span: 8 }}
          wrapperCol={{ span: 16 }}
          initialValues={selectedRecord}
          labelAlign="left"
          form={form}
          onFinish={onFinish}
        >
          <Form.Item label="id" name="id" noStyle>
            <Input type="hidden" />
          </Form.Item>
          <Form.Item name="userName" label="T??n t??i kho???n">
            <Input
              disabled={Object.keys(selectedRecord).length !== 0 ? true : false}
            />
          </Form.Item>
          {Object.keys(selectedRecord).length === 0 && (
            <Form.Item name="password" label="M???t kh???u">
              <Input />
            </Form.Item>
          )}
          <Form.Item name="scopeId" label="Nh??m quy???n">
            <Select>
              {scopes.map((item, index) => (
                <Option value={item.id} key={index}>
                  {item.name}
                </Option>
              ))}
            </Select>
          </Form.Item>
          <Form.Item name="unitIds" label="????n v??? qu???n l??">
            <Select mode="multiple" allowClear>
              {units.map((item, index) => (
                <Option value={item.id} key={index}>
                  {item.name}
                </Option>
              ))}
            </Select>
          </Form.Item>
          <Form.Item
            wrapperCol={{
              offset: 8,
              span: 16,
            }}
          >
            <Button type="primary" htmlType="submit">
              Submit
            </Button>
          </Form.Item>
        </Form>
      </Drawer>
      <Modal
        visible={modalVisible}
        onOk={() => {
          modalForm.submit()
        }}
        title={
          <>
            ?????i m???t kh???u t??i kho???n{" "}
            <Text keyboard>{selectedRecord.userName}</Text>
          </>
        }
        onCancel={() => {
          setModalVisible(false)
          setSelectedRecord({})
        }}
        okText="L??u"
        cancelText="Hu???"
      >
        <Form
          labelCol={{ span: 8 }}
          wrapperCol={{ span: 16 }}
          labelAlign="left"
          form={modalForm}
          onFinish={onSavePassword}
          style={{ marginTop: 20 }}
        >
          <Form.Item label="id" name="id" hidden>
            <Input />
          </Form.Item>
          <Form.Item label="T??n ????ng nh???p" name="userName" hidden>
            <Input type="email" />
          </Form.Item>
          <Form.Item label="M???t kh???u m???i" name="password">
            <Input type="text" />
          </Form.Item>
          <Form.Item label="Nh??m quy???n" name="scopeId" hidden>
            <Input />
          </Form.Item>
          <Form.Item name="unitIds" label="????n v??? qu???n l??" hidden>
            <Select mode="multiple" allowClear>
              {units.map((item, index) => (
                <Option value={item.id} key={index}>
                  {item.name}
                </Option>
              ))}
            </Select>
          </Form.Item>
        </Form>
      </Modal>
      <CustomSkeleton loading={loading} />
    </>
  )
}

export default User
